using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class OrdenesCompra
{
    public int OrdenCompraId { get; set; }

    public string NumeroOrden { get; set; } = null!;

    public int ProveedorId { get; set; }

    public int? ContratoId { get; set; }

    public DateOnly FechaEmision { get; set; }

    public DateOnly? FechaEntregaEsp { get; set; }

    public decimal MontoTotal { get; set; }

    public int? UsuarioCreadorId { get; set; }

    public string? Observaciones { get; set; }

    public virtual Contrato? Contrato { get; set; }

    public virtual ICollection<CuentasPagar> CuentasPagars { get; set; } = new List<CuentasPagar>();

    public virtual Proveedore Proveedor { get; set; } = null!;

    public virtual ICollection<RecepcionMercaderium> RecepcionMercaderia { get; set; } = new List<RecepcionMercaderium>();

    public virtual Usuario? UsuarioCreador { get; set; }
}
