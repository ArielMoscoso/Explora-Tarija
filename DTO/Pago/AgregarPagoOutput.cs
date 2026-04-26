using System;
using ExploraTarija.Entidades;

namespace ExploraTarija.DTO.Pago.AgregarPago;

    public class AgregarPagoOutput  
    {
        public int IdPago { get; set; }
        public string? MetodoPago { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }

        public EstadoPago? Estado { get; set; }

        public int IdReserva { get; set; }
        
    }
