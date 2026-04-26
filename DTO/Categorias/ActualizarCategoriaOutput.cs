using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ExploraTarija.DTO.Categoria.ActualizarCategoria;

public class ActualizarCategoriaOutput
{
    public int IdCategoria { get; set; }
    public string? NombreCategoria { get; set; }
    public DateTime FechaModificacionCategoria { get; set; }
}