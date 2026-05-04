using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Usuario.ActualizarUsuario;

public class ActualizarUsuarioInput
{
    
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(15, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 15 caracteres.")]
    public required string nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres.")]
    public required string apellido { get; set; }

    [DefaultValue(0)]
    [Range(1, 99999999, ErrorMessage = "El CI debe ser mayor a 0.")]
    public int CI { get; set; }

    [DefaultValue(0)]
    [Range(10000000, 99999999, ErrorMessage = "Ingrese un número de celular válido de Bolivia.")]
    public int Celular { get; set; }
}