using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.DTO.Usuario.AgregarUsuario;
using ExploraTarija.Data;
using ExploraTarija.Entidades;

namespace ExploraTarija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
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
        public async Task<IActionResult> UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuarios) return BadRequest();
            _contexto.Entry(usuario).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
        // Borrar usuario

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}