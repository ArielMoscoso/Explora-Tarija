using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Usuario.ActualizarUsuario;

public class ActualizarUsuarioOutput
{
    public int IdUsuario { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public  int CI { get; set; }
    public int Celular { get; set; }
    public DateTime FechaModificacionUsuario { get; set; }
}