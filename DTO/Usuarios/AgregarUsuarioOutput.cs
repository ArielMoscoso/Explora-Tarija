using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Usuario.AgregarUsuario;

public class AgregarUsuarioOutput
{
    public int IdUsuario { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public  int CI { get; set; }
    public int Celular { get; set; }
}