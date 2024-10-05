using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial2024.Models
{
    public class Remesa
    {
        public int Id { get; set; }
        
        public string? Remitente { get; set; }
        
        public string? Destinatario { get; set; }
        
        public string? PaisOrigen { get; set; }
        
        public string? PaisDestino { get; set; }
        
        public decimal MontoEnviado { get; set; }
        
        public string? MonedaEnviada { get; set; } // "USD" o "BTC"
        
        public decimal TasaCambio { get; set; } // Tasa entre USD y BTC
        
        public decimal MontoFinal { get; set; } // Monto en moneda de destino
        
        public string? EstadoTransaccion { get; set; } // Ejemplo: "Pendiente", "Completada", "Cancelada"
    }
}