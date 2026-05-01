using System;

namespace ExploraTarija.DTO.Pago.AgregarPago
{
    public class AgregarPagoInput
    {
       
        public required string MetodoPago { get; set; }
        public decimal Monto { get; set; }
        public int IdReserva { get; set; }
        
    }

}