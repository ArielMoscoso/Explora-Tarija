using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Categoria.AgregarCategoria;
public class AgregarCategoriaOutput
{
     public int IdCategoria {get; set;}
     public string? NombreCategoria {get; set;} 
}