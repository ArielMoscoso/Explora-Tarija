using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace ExploraTarija.Entidades
{
    public class Catalogo
    {
        public int IdCatalogo { get; set; }
        public string? Titulo { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaPublicacion { get; set; }

        public int IdEmpresa { get; set; }
        public Empresa? EmpresaPropietaria { get; set; }

        public int IdCategoria { get; set; }
        public Categoria? CategoriaAsignada { get; set; }

    }
}