using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.Data;
using ExploraTarija.Entidades;

namespace ExploraTarija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public CategoriasController(AppDbContext contexto) => _contexto = contexto;

        // 1. LEER TODAS (GET)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias() => 
            Ok(await _contexto.Categorias.ToListAsync());

        // 2. LEER UNA POR ID (GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);
            return categoria == null ? NotFound() : Ok(categoria);
        }

        // 3. AÑADIR NUEVA (POST)
        [HttpPost]
        public async Task<ActionResult<Categoria>> CreateCategoria(Categoria categoria)
        {
            _contexto.Categorias.Add(categoria);
            await _contexto.SaveChangesAsync();
            
            // Retorna la ruta del nuevo objeto creado
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.IdCategoria }, categoria);
        }

        // 4. ACTUALIZAR (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, Categoria categoria)
        {
            // Validación de seguridad: el ID de la URL debe coincidir con el del cuerpo
            if (id != categoria.IdCategoria) return BadRequest("El ID no coincide.");

            _contexto.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contexto.Categorias.Any(e => e.IdCategoria == id)) return NotFound();
                else throw;
            }

            return NoContent(); // 204: Éxito sin contenido de respuesta
        }

        // 5. BORRAR (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();

            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}