using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ExploraTarija.DTO.Empresa.AgregarEmpresa;
public class AgregarEmpresaInput
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
    public required string NombreEmpresa { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    public required int TelefonoEmpresa { get; set; }
    [EmailAddress(ErrorMessage = "Correo inválido.")]
    public string? CorreoEmpresa { get; set; }
    public string? DireccionEmpresa { get; set; }
}