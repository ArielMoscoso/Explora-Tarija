using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.DTO.Usuario.AgregarUsuario;
using ExploraTarija.DTO.Usuario.ActualizarUsuario;
using ExploraTarija.DTO.Usuario.EliminarUsuario;
using ExploraTarija.Data;
using ExploraTarija.Entidades;
using ExploraTarija.DTO.Usuario.FiltrarUsuario;

namespace ExploraTarija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : BaseApiController
    {
        private readonly AppDbContext _contexto;

        public UsuariosController(AppDbContext contexto) => _contexto = contexto;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios() => 
            Ok(await _contexto.Usuarios.ToListAsync());

        [HttpGet("FiltrarUsuario")]
        public async Task<IActionResult> GetUsuario(
            [FromQuery] string? nombreUsuario)
        {
            var query = _contexto.Usuarios.AsQueryable();
            if (!string.IsNullOrEmpty(nombreUsuario))
            {
                query = query.Where(u => u.Nombre.Contains(nombreUsuario));
            }
            
            var usuarios = await query.Select(u => new FiltrarUsuarioOutput
            {
                IdUsuario = u.IdUsuarios,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                CI = u.CI,
                Celular = u.Celular
            }).ToListAsync();
            return Ok(usuarios);
        }
        // Crea usuario

       [HttpPost]
        public async Task<ActionResult<AgregarUsuarioOutput>> CreateUsuario([FromBody] AgregarUsuarioInput usuarioInput)
        {
            var nombreNormalizado = NormalizarTexto(usuarioInput.nombre);
            var apellidoNormalizado = NormalizarTexto(usuarioInput.apellido);

            var existeUsuario = await _contexto.Usuarios.AnyAsync(x => x.CI == usuarioInput.CI);
            if (existeUsuario)
                return Conflict("Ya existe un usuario registrado con este CI.");

            // Mapeo de Entrada a la Entidad de BD
            var nuevoUsuario = new Usuario
            {
                Nombre = usuarioInput.nombre,
                Apellido = usuarioInput.apellido,
                CI = usuarioInput.CI,
                Celular = usuarioInput.Celular
            };

            // Guarda en la base de datos
            _contexto.Usuarios.Add(nuevoUsuario);
            await _contexto.SaveChangesAsync();

            // Salida Output
            var salida = new AgregarUsuarioOutput
            { 
                IdUsuario = nuevoUsuario.IdUsuarios,
                Nombre = nuevoUsuario.Nombre,
                Apellido = nuevoUsuario.Apellido,
                CI = nuevoUsuario.CI,
                Celular = nuevoUsuario.Celular
            };

            return CreatedAtAction(nameof(GetUsuario), new { id = salida.IdUsuario }, salida);
        }
    
        // Actualiza usuario

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] ActualizarUsuarioInput usuarioInput )
        {
            if (id <= 0)
                return BadRequest("El ID del usuario es inválido.");

            var existing = await _contexto.Usuarios.FindAsync(id);
            
            if (existing == null)
                return NotFound();

            
            var ciEnUso = await _contexto.Usuarios.AnyAsync(x => x.IdUsuarios != id && x.CI == usuarioInput.CI);;
            if (ciEnUso)
                return Conflict("El CI ingresado ya pertenece a otro usuario.");

            
            existing.Nombre = NormalizarTexto(usuarioInput.nombre);
            existing.Apellido = NormalizarTexto(usuarioInput.apellido);
            existing.CI = usuarioInput.CI;
            existing.Celular = usuarioInput.Celular;

            await _contexto.SaveChangesAsync();

            
            var salida = new ActualizarUsuarioOutput
            {
                IdUsuario = existing.IdUsuarios,
                Nombre = existing.Nombre, 
                Apellido = existing.Apellido,
                CI = existing.CI,
                Celular = existing.Celular,
                FechaModificacionUsuario = DateTime.Now
            };

            return Ok(salida);
        }
        
        private static string  NormalizarTexto(string texto)
        {
            return texto.Trim().ToUpperInvariant();
        }
    }
}