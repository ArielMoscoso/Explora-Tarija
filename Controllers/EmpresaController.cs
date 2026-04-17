using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.Data;
using ExploraTarija.Entidades;

namespace ExploraTarija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public EmpresasController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> GetEmpresas()
        {
            return Ok(await _contexto.Empresas.ToListAsync());
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> GetEmpresa(int id)
        {
            var empresa = await _contexto.Empresas.FindAsync(id);
            if (empresa == null) return NotFound("La empresa no existe.");
            return Ok(empresa);
        }

        // Crea empresa
        [HttpPost]
        public async Task<ActionResult<Empresa>> CreateEmpresa([FromBody] Empresa empresa)
        {
            _contexto.Empresas.Add(empresa);
            await _contexto.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.IdEmpresa }, empresa);
        }

        // Actualiza empresa
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(int id, [FromBody] Empresa empresa)
        {
            if (id != empresa.IdEmpresa)
                return BadRequest("El ID de la URL no coincide con el ID del cuerpo.");

            var empresaExistente = await _contexto.Empresas.FindAsync(id);
            if (empresaExistente == null) return NotFound("Empresa no encontrada.");

            
            empresaExistente.NombreEmpresa = empresa.NombreEmpresa;
            empresaExistente.TelefonoEmpresa = empresa.TelefonoEmpresa;
            empresaExistente.CorreoEmpresa = empresa.CorreoEmpresa;
            empresaExistente.DireccionEmpresa = empresa.DireccionEmpresa;

            await _contexto.SaveChangesAsync();
            return NoContent(); 
        }

        // Borrar empresa
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            var empresa = await _contexto.Empresas.FindAsync(id);
            if (empresa == null) return NotFound("No se encontró la empresa para eliminar.");

            _contexto.Empresas.Remove(empresa);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}