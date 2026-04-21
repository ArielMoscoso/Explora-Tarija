using System;
using System.Security.Authentication;

namespace ExploraTarija.DTO.Usuario.AgregarUsuario;

public class AgregarUsuarioInput
{
    public required string nombre { get; set; }
    public required string apellido { get; set; }
    public int CI { get; set; }
    public int Celular { get; set; }
}