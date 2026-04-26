using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Empresa.ActualizarEmpresa;
public class ActualizarEmpresaOutput
{
    public int IdEmpresa {get; set;}
    public required string NombreEmpresa {get; set;} 
    public int TelefonoEmpresa {get; set;}

    public string? CorreoEmpresa {get; set;}
    public string? DireccionEmpresa {get; set;}
    public DateTime FechaModificacionEmpresa {get; set;}
}
