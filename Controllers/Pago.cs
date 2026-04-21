using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.Data;
using ExploraTarija.Entidades;

namespace ExploraTarija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public PagosController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        // Get
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            
            var pago = await _contexto.Pagos
                .Include(p => p.Reserva) 
                .FirstOrDefaultAsync(p => p.IdPago == id);

            if (pago == null) return NotFound("El pago no existe.");

            return Ok(pago);
        }

        // Post
        [HttpPost]
        public async Task<ActionResult<Pago>> CreatePago([FromBody] Pago pago)
        {
            
            pago.FechaPago = DateTime.Now; 

           
            _contexto.Pagos.Add(pago);
            await _contexto.SaveChangesAsync();

            
            return CreatedAtAction(nameof(GetPago), new { id = pago.IdPago }, pago);
        }
    }
}
