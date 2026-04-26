using System;

namespace ExploraTarija.Entidades
{
    public class Usuario
    {
        public  int IdUsuarios { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public int CI { get; set; }
        public int Celular { get; set; }

        public List<Reserva>? MisReservas { get; set; }
    }
}