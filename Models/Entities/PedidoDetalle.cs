using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaginaRefrescosDelValle.Models.Entities
{
    public partial class PedidoDetalle
    {
        [Key]
        public int PedidoDetalleId { get; set; }

        public int PedidoId { get; set; }

        public int ProductoId { get; set; }

        public int? PresentacionId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Cantidad { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Descuento { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Subtotal { get; set; }

        // --- RELACIONES (Propiedades de Navegación) ---

        public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

        [ForeignKey("PedidoId")]
        public virtual Pedido Pedido { get; set; } = null!;

        // Nota: Asegúrate de que la clase se llame 'Presentacion' o 'Presentacione' en tu proyecto
        [ForeignKey("PresentacionId")]
        public virtual Presentacione? Presentacion { get; set; }

        [ForeignKey("ProductoId")]
        public virtual Producto Producto { get; set; } = null!;
    }
}