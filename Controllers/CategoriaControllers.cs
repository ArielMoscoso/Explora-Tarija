using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.Data;
using ExploraTarija.DTO.Categoria.AgregarCategoria;
using ExploraTarija.DTO.Categoria.ActualizarCategoria ;
using ExploraTarija.DTO.Categoria.FiltrarCategoria;
using ExploraTarija.DTO.Categoria.EliminarCategoria;
using ExploraTarija.Entidades;

namespace ExploraTarija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : BaseApiController
    {
        private readonly AppDbContext _contexto;

        public CategoriasController(AppDbContext contexto) => _contexto = contexto;

        // 1. LEER TODAS (GET)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias() => 
            Ok(await _contexto.Categorias.ToListAsync());

        // 2. LEER UNA POR ID (GET) run
        [HttpGet("FiltrarCategoria")]
        public async Task<IActionResult> GetCategoria(
            [FromQuery] string? nombreCategoria)

        {
            var query = _contexto.Categorias.AsQueryable();
            if (!string.IsNullOrEmpty(nombreCategoria))
            {
                query = query.Where(c => c.NombreCategoria.Contains(nombreCategoria));
            }
            var categorias = await query.Select(c => new FiltrarCategoriaOutput
            {
                IdCategoria = c.IdCategoria,
                NombreCategoria = c.NombreCategoria
            }).ToListAsync();
            return Ok(categorias);
        }

        // 3. AÑADIR NUEVA (POST)
        [HttpPost]

        public async Task<ActionResult<AgregarCategoriaOutput>> CreateCategoria([FromBody] AgregarCategoriaInput categoriaInput)
        {
            var nuevaCategoria = new Categoria
            {
                NombreCategoria = NormalizarTexto(categoriaInput.NombreCategoria)   
            };
            
            _contexto.Categorias.Add(nuevaCategoria);
            await _contexto.SaveChangesAsync();

            var salida = new AgregarCategoriaOutput
            {
                IdCategoria = nuevaCategoria.IdCategoria,
                NombreCategoria = nuevaCategoria.NombreCategoria
            };

            
            // Retorna la ruta del nuevo objeto creado
            return CreatedAtAction(nameof(GetCategoria), new { id = salida.IdCategoria }, salida);
        }

        // 4. ACTUALIZAR (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] ActualizarCategoriaInput categoriaInput)
        {

            
            var categoriaExistente = await _contexto.Categorias.FindAsync(id);
            if (categoriaExistente == null) return NotFound("La categoría no existe.");

            categoriaExistente.IdCategoria = categoriaInput.IdCategoria;
            categoriaExistente.NombreCategoria = categoriaInput.NombreCategoria;

            await _contexto.SaveChangesAsync();

            var salida = new Categoria
            {
                IdCategoria = categoriaExistente.IdCategoria,
                NombreCategoria = categoriaExistente.NombreCategoria
            };

            return Ok(salida); // 204: Éxito sin contenido de respuesta
        }

        private static string NormalizarTexto(string texto)
        {
            return texto.Trim().ToUpperInvariant();
        }

    }
}