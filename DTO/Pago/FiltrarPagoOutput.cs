using System;

namespace ExploraTarija.DTO.Pago.FiltrarPago;
public class FiltrarPagoOutput
{
    public int IdPago { get; set; }
    public required string MetodoPago { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaPago { get; set; }
}