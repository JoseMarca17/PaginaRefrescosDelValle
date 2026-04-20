using System;
using System.Collections.Generic;
using PaginaRefrescosDelValle.Models.Entities;
namespace RefrescosDelValle.Models.Entities;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public string NumeroPedido { get; set; } = null!;

    public int ClienteId { get; set; }

    public DateTime FechaPedido { get; set; }

    public DateOnly? FechaEntregaEsp { get; set; }

    public string EstadoPedido { get; set; } = null!;

    public int? UsuarioVendedorId { get; set; }

    public int? SucursalId { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

    public virtual Sucursale? Sucursal { get; set; }

    public virtual Usuario? UsuarioVendedor { get; set; }
}
