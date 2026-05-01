using System;

namespace ExploraTarija.Entidades
{
    public class Pago
    {
        public int IdPago { get; set; }
        public string MetodoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }

        public EstadoPago? Estado { get; set; }

        public int IdReserva { get; set; }
        public Reserva? Reserva { get; set; }
    }
}