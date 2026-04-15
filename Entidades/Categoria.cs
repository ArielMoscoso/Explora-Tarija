using System;

namespace ExploraTarija.Entidades
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
    // Relación: Una categoría tiene muchos productos en el catálogo
        public List<Catalogo> ItemsCatalogo { get; set; }
    }
}