using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Proveedore
{
    public int ProveedorId { get; set; }

    public int PersonaId { get; set; }

    public string? RazonSocial { get; set; }

    public string? Nit { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual ICollection<CuentasPagar> CuentasPagars { get; set; } = new List<CuentasPagar>();

    public virtual ICollection<OrdenesCompra> OrdenesCompras { get; set; } = new List<OrdenesCompra>();

    public virtual Persona Persona { get; set; } = null!;
}
