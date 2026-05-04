using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Categoria.ActualizarCategoria;
public class ActualizarCategoriaInput
{
    [Required(ErrorMessage = "El id de categoría es obligatorio.")]
    public int IdCategoria { get; set; }
    
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public required string NombreCategoria {get; set;}
}