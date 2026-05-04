using System;

namespace ExploraTarija.DTO.Empresa.FiltrarEmpresa;
public class FiltrarEmpresaOutput
{
    public int IdEmpresa { get; set; }
    public string? NombreEmpresa { get; set; }
    public int TelefonoEmpresa { get; set; }
    public string? CorreoEmpresa { get; set; }
    public string? DireccionEmpresa { get; set; }
}