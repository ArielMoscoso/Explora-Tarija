using System;

namespace ExploraTarija.Entidades
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public required string NombreEmpresa { get; set; }
        public int TelefonoEmpresa { get; set; }
        public string? CorreoEmpresa { get; set; }
        public string? DireccionEmpresa { get; set; }

        public List<Catalogo>? Productos { get; set; }
    }
}