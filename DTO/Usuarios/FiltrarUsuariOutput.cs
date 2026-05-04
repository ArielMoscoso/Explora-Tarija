using System;

namespace ExploraTarija.DTO.Usuario.FiltrarUsuario;

public class FiltrarUsuarioOutput
{
    public int IdUsuario{get; set;}
    public string? Nombre {get; set; }
    public string? Apellido {get; set; }
    public int CI { get; set; }
    public int Celular { get; set; }
    
}
