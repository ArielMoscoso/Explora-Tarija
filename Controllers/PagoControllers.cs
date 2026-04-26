using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.Data;
using ExploraTarija.DTO.Pago.AgregarPago;
using ExploraTarija.Entidades;

namespace ExploraTarija.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : BaseApiController
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
        public async Task<ActionResult<AgregarPagoOutput>> CreatePago([FromBody] AgregarPagoInput usuarioInput)
        {
            // Validamos si la reserva existe antes de pagar
            var existeReserva = await _contexto.Reservas.AnyAsync(x => x.IdReserva == usuarioInput.IdReserva);
            if (!existeReserva)
                return NotFound("La reserva especificada no existe.");

            var nuevoPago = new Pago
            {
                Monto = usuarioInput.Monto,
                MetodoPago = AdecuarTexto(usuarioInput.MetodoPago),
                FechaPago = DateTime.Now,
                IdReserva = usuarioInput.IdReserva,
                Estado = EstadoPago.Completado 
            };

            _contexto.Pagos.Add(nuevoPago);
            await _contexto.SaveChangesAsync();

            var salida = new AgregarPagoOutput
            {
                IdPago = nuevoPago.IdPago,
                Monto = nuevoPago.Monto,
                Estado = nuevoPago.Estado, 
                FechaPago = nuevoPago.FechaPago,
                IdReserva = nuevoPago.IdReserva
                
            };



            return Ok(salida);
        }
        private static string? AdecuarTexto(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;
            return texto.Trim().ToUpperInvariant();
        }
    }
    
 }

