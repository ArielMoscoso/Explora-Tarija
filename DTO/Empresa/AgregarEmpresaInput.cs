using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ExploraTarija.DTO.Empresa.AgregarEmpresa;
public class AgregarEmpresaInput
{
    public required string NombreEmpresa { get; set; }
    public int TelefonoEmpresa { get; set; }
    public string? CorreoEmpresa { get; set; }
    public string? DireccionEmpresa { get; set; }
}