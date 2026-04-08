using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class FacturaDetalle
{
    public int FacturaDetalleId { get; set; }

    public int FacturaId { get; set; }

    public int PedidoDetalleId { get; set; }

    public decimal Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal? Subtotal { get; set; }

    public virtual Factura Factura { get; set; } = null!;

    public virtual PedidoDetalle PedidoDetalle { get; set; } = null!;
}
