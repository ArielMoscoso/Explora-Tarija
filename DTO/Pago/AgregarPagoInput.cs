using System;
using System.ComponentModel.DataAnnotations;

namespace ExploraTarija.DTO.Pago.AgregarPago
{
    public class AgregarPagoInput
    {
        [Required(ErrorMessage = "El metodo de pago es obligatorio")]
        [StringLength(10,MinimumLength = 2, ErrorMessage = "El metodo de pago no puede exceder los 10 caracteres")]
        public required string MetodoPago { get; set; }

        [Range(1, 999, ErrorMessage = "El monto debe ser un valor positivo y no puede exceder los 999")]
        public decimal Monto { get; set; }
        
        public int IdReserva { get; set; }
        
    }

}