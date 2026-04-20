using System;
using System.Collections.Generic;

namespace PaginaRefrescosDelValle.Models.ViewModels
{
    // Esta clase une todos los datos que se ven en tu pantalla principal de Ventas
    public class VentasDashboardViewModel
    {
        public decimal VentasDelMes { get; set; }
        public decimal CobradoDelMes { get; set; }
        public decimal PorCobrar { get; set; }
        
        // Listado de los últimos pedidos para la tabla
        public List<PedidoResumenViewModel> PedidosRecientes { get; set; } = new();
        
        // Listado de facturas que ya pasaron su fecha de vencimiento
        public List<FacturaResumenViewModel> FacturasVencidas { get; set; } = new();
    }

    // Estructura simplificada para mostrar pedidos en la tabla
    public class PedidoResumenViewModel
    {
        public int PedidoID { get; set; }
        public string Cliente { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = null!;
        public decimal Total { get; set; }
    }

    // Estructura para mostrar las alertas de facturas vencidas
    public class FacturaResumenViewModel
    {
        public int FacturaID { get; set; }
        public string NumeroFactura { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public DateTime Vencimiento { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
    }
}