using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExploraTarija.Data;
using ExploraTarija.DTO.Pago.AgregarPago;
using ExploraTarija.DTO.Pago.FiltrarPago;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos() => 
            Ok(await _contexto.Pagos.ToListAsync());

        // Get
        [HttpGet("FiltrarMetodoPago")]
        public async Task<IActionResult> GetPago(
            [FromQuery] string? metodoPago)
        {
            var query = _contexto.Pagos.AsQueryable();
            if (!string.IsNullOrEmpty(metodoPago))
            {
                query = query.Where(p => p.MetodoPago.Contains(metodoPago));
            }
            var pagos = await query.Select(p => new FiltrarPagoOutput
            {
                IdPago = p.IdPago,
                Monto = p.Monto,
                MetodoPago = p.MetodoPago,
                FechaPago = p.FechaPago
            }).ToListAsync();
            return Ok(pagos);
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
                MetodoPago = NormalizarTexto(usuarioInput.MetodoPago),
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
        private static string NormalizarTexto(string texto)
        {
            
            return texto.Trim().ToUpperInvariant();
        }
    }
    
 }

