using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.DTO.Usuario.AgregarUsuario;
using ExploraTarija.DTO.Usuario.ActualizarUsuario;
using ExploraTarija.DTO.Usuario.EliminarUsuario;
using ExploraTarija.Data;
using ExploraTarija.Entidades;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            return usuario == null ? NotFound() : Ok(usuario);
        }
        // Crea usuario

       [HttpPost]
        public async Task<ActionResult<AgregarUsuarioOutput>> CreateUsuario([FromBody] AgregarUsuarioInput input)
        {
            var nombreNormalizado = NormalizarTexto(input.nombre);
            var apellidoNormalizado = NormalizarTexto(input.apellido);

            var existeUsuario = await _contexto.Usuarios.AnyAsync(x => x.CI == input.CI);
            if (existeUsuario)
                return Conflict("Ya existe un usuario registrado con este CI.");

            // Mapeo de Entrada a la Entidad de BD
            var nuevoUsuario = new Usuario
            {
                Nombre = input.nombre,
                Apellido = input.apellido,
                CI = input.CI,
                Celular = input.Celular
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
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] ActualizarUsuarioInput input )
        {
            if (id <= 0)
                return BadRequest("El ID del usuario es inválido.");

            var existing = await _contexto.Usuarios.FindAsync(id);
            
            if (existing == null)
                return NotFound();

            
            var ciEnUso = await _contexto.Usuarios.AnyAsync(x => x.IdUsuarios != id && x.CI == input.CI);;
            if (ciEnUso)
                return Conflict("El CI ingresado ya pertenece a otro usuario.");

            
            existing.Nombre = NormalizarTexto(input.nombre);
            existing.Apellido = NormalizarTexto(input.apellido);
            existing.CI = input.CI;
            existing.Celular = input.Celular;

            await _contexto.SaveChangesAsync();

            
            var salida = new ActualizarUsuarioOutput
            {
                IdUsuario = existing.IdUsuarios,
                Nombre = existing.Nombre, 
                Apellido = existing.Apellido,
                CI = existing.CI,
                Celular = existing.Celular,
                FechaActualizacion = DateTime.Now
            };

            return Ok(salida);
        }
        
        // Elimina usuario
        [HttpDelete("{id}")]
        public async Task<ActionResult<EliminarUsuarioOutput>> DeleteUsuario(int id)
        {
            if (id <= 0)
                return BadRequest("El ID del usuario es inválido.");

            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

           
            var salida = new EliminarUsuarioOutput
            {
                IdUsuario = usuario.IdUsuarios,
                Nombre = usuario.Nombre,
                Mensaje = "Usuario eliminado exitosamente."
            };

            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return Ok(salida);
        }
        private static string? NormalizarTexto(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return null;

            return texto.Trim().ToUpperInvariant();
        }
    }
}