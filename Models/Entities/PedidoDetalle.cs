using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class PedidoDetalle
{
    public int PedidoDetalleId { get; set; }

    public int PedidoId { get; set; }

    public int ProductoId { get; set; }

    public int? PresentacionId { get; set; }

    public decimal Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Descuento { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual Presentacione? Presentacion { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
