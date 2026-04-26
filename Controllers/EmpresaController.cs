using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.Data;
using ExploraTarija.Entidades;
using ExploraTarija.DTO.Empresa.ActualizarEmpresa;
using ExploraTarija.DTO.Empresa.AgregarEmpresa;
using ExploraTarija.DTO.Empresa.EliminarEmpresa;

namespace ExploraTarija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : BaseApiController
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
        public async Task<ActionResult<AgregarEmpresaOutput>> CreateEmpresa([FromBody] AgregarEmpresaInput empresaInput)
        {
            
            var existeEmpresa = await _contexto.Empresas.AnyAsync(x => x.NombreEmpresa == empresaInput.NombreEmpresa);
            if (existeEmpresa)
                return Conflict("Ya existe una empresa registrada con este nombre.");
            

            var nuevaEmpresa = new Empresa
            {
                NombreEmpresa = empresaInput.NombreEmpresa,
                TelefonoEmpresa = empresaInput.TelefonoEmpresa,
                CorreoEmpresa = empresaInput.CorreoEmpresa,
                DireccionEmpresa = empresaInput.DireccionEmpresa
            };
            _contexto.Empresas.Add(nuevaEmpresa);
            await _contexto.SaveChangesAsync();

            var salida = new AgregarEmpresaOutput
            {
                IdEmpresa = nuevaEmpresa.IdEmpresa,
                NombreEmpresa = nuevaEmpresa.NombreEmpresa,
                TelefonoEmpresa = nuevaEmpresa.TelefonoEmpresa,
                CorreoEmpresa = nuevaEmpresa.CorreoEmpresa,
                DireccionEmpresa = nuevaEmpresa.DireccionEmpresa
            };
            return CreatedAtAction(nameof(GetEmpresa), new { id = salida.IdEmpresa }, salida);
        }

        // Actualiza empresa
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(int id, [FromBody] ActualizarEmpresaInput empresa)
        {
            var empresaExistente = await _contexto.Empresas.FindAsync(id);
            
            
            if (empresaExistente == null) return NotFound("Empresa no encontrada.");

            
            empresaExistente.NombreEmpresa = empresa.NombreEmpresa;
            empresaExistente.TelefonoEmpresa = empresa.TelefonoEmpresa;
            empresaExistente.CorreoEmpresa = empresa.CorreoEmpresa;
            empresaExistente.DireccionEmpresa = empresa.DireccionEmpresa;

            await _contexto.SaveChangesAsync();

            var salida = new ActualizarEmpresaOutput
            
            {
                
                IdEmpresa = empresaExistente.IdEmpresa,
                NombreEmpresa = empresaExistente.NombreEmpresa,
                TelefonoEmpresa = empresaExistente.TelefonoEmpresa,
                CorreoEmpresa = empresaExistente.CorreoEmpresa,
                DireccionEmpresa = empresaExistente.DireccionEmpresa,
            };
            return NoContent(); 
        }

        // Borrar empresa
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            if (id <= 0)
                return BadRequest("El ID de la empresa es invalido.");
            
            var empresa = await _contexto.Empresas.FindAsync(id);
            if (empresa == null) return NotFound("No se encontró la empresa para eliminar.");

            var salida = new EliminarEmpresaOutput
            {
                IdEmpresa = empresa.IdEmpresa
            };

            _contexto.Empresas.Remove(empresa);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}