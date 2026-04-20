using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaginaRefrescosDelValle.Models.Entities
{
    public partial class FacturaDetalle
    {
        [Key]
        public int FacturaDetalleId { get; set; }

        public int FacturaId { get; set; }

        public int PedidoDetalleId { get; set; }

        public decimal Cantidad { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioUnitario { get; set; }

        public decimal? Subtotal { get; set; }

        // --- RELACIONES (Propiedades de Navegación) ---

        [ForeignKey("FacturaId")]
        public virtual Facturas Facturas { get; set; } = null!;

        [ForeignKey("PedidoDetalleId")]
        public virtual PedidoDetalle PedidoDetalle { get; set; } = null!;
    }
}