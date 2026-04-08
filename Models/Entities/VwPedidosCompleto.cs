using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class VwPedidosCompleto
{
    public int PedidoId { get; set; }

    public string NumeroPedido { get; set; } = null!;

    public DateTime FechaPedido { get; set; }

    public string EstadoPedido { get; set; } = null!;

    public string NombreCliente { get; set; } = null!;

    public string? TelefonoPrincipal { get; set; }

    public string? CorreoPrincipal { get; set; }

    public string? TipoCliente { get; set; }

    public string NombreVendedor { get; set; } = null!;

    public string? NombreSucursal { get; set; }

    public DateOnly? FechaEntregaEsp { get; set; }
}
