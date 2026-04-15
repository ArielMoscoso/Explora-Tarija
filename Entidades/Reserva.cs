using System;

namespace ExploraTarija.Entidades
{
    public class Reserva
    {
        public int IdReservas { get; set; }
        public DateTime FechaReserva { get; set; }

        public int IdUsuario { get; set; }
        public Usuario UsuarioReserva { get; set; }

        public int IdCatalogo { get; set; }
        public Catalogo ProductoReservado { get; set; }

        public int IdPago { get; set; }
        public Pago DetallePago { get; set; }
    }
}