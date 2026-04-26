using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Usuario.ActualizarUsuario;

public class ActualizarUsuarioInput
{
    // El ID no suele validarse con DataAnnotations aquí porque viene de la URL
    
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public required string nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres.")]
    public required string apellido { get; set; }

    [DefaultValue(0)]
    [Range(1, int.MaxValue, ErrorMessage = "El CI debe ser mayor a 0.")]
    public int CI { get; set; }

    [DefaultValue(0)]
    [Range(60000000, 79999999, ErrorMessage = "Ingrese un número de celular válido de Bolivia.")]
    public int Celular { get; set; }
}