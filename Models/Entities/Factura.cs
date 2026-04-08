using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Factura
{
    public int FacturaId { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public int PedidoId { get; set; }

    public int ClienteId { get; set; }

    public DateTime FechaEmision { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Descuento { get; set; }

    public decimal Impuesto { get; set; }

    public decimal? Total { get; set; }

    public bool Anulada { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<FacturaDetalle> FacturaDetalles { get; set; } = new List<FacturaDetalle>();

    public virtual ICollection<PagosCliente> PagosClientes { get; set; } = new List<PagosCliente>();

    public virtual Pedido Pedido { get; set; } = null!;
}
