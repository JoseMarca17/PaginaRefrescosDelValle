using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Contrato
{
    public int ContratoId { get; set; }

    public int ProveedorId { get; set; }

    public string NumeroContrato { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal MontoTotal { get; set; }

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<OrdenesCompra> OrdenesCompras { get; set; } = new List<OrdenesCompra>();

    public virtual Proveedore Proveedor { get; set; } = null!;
}
