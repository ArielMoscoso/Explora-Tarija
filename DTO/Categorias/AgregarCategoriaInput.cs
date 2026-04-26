using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Categoria.AgregarCategoria;
public class AgregarCategoriaInput
{
    public required string NombreCategoria {get; set;}
}