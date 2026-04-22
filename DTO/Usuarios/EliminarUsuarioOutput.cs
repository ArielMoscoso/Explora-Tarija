using System;

namespace ExploraTarija.DTO.Usuario.EliminarUsuario;

public class EliminarUsuarioOutput
{
    public int IdUsuario { get; set; }
    public string? Nombre { get; set; }
    public string? Mensaje { get; set; } = "Usuario eliminado exitosamente.";

}
