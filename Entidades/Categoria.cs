using System;

namespace ExploraTarija.Entidades
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string? NombreCategoria { get; set; }
        public List<Catalogo> ItemsCatalogo { get; set; }
    }
}