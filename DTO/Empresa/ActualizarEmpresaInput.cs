using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Empresa.ActualizarEmpresa;
public class ActualizarEmpresaInput
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public required string NombreEmpresa {get; set;}

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    public int TelefonoEmpresa {get; set;}

    [EmailAddress(ErrorMessage = "Correo inválido.")]
    public string? CorreoEmpresa {get; set;}
    
    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "La dirección debe tener entre 3 y 80 caracteres.")]
    public string? DireccionEmpresa {get; set;}

    
}