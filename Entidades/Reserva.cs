using System;

namespace ExploraTarija.Entidades
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public DateTime FechaReserva { get; set; }
        
        public EstadoReserva? Estado { get; set; }

        public int IdUsuario { get; set; }
        public Usuario? UsuarioReserva { get; set; }

        public int IdCatalogo { get; set; }
        public Catalogo? ProductoReservado { get; set; }

        public List<Pago>? Pagos { get; set; }
    }
} 