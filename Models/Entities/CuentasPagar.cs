using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class CuentasPagar
{
    public int CuentaPagarId { get; set; }

    public int ProveedorId { get; set; }

    public int? OrdenCompraId { get; set; }

    public decimal Monto { get; set; }

    public DateOnly FechaEmision { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public bool Pagado { get; set; }

    public DateOnly? FechaPago { get; set; }

    public string? Observaciones { get; set; }

    public virtual OrdenesCompra? OrdenCompra { get; set; }

    public virtual Proveedore Proveedor { get; set; } = null!;
}
