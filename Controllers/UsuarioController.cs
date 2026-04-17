using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuarios }, usuario);
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