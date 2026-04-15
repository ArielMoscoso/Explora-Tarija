using System;

namespace ExploraTarija.Entidades
{
    public class Pago
    {
        public required int IdPago { get; set; }
        public required string MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
    }
}