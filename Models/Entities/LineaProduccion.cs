using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class LineaProduccion
{
    public int LineaId { get; set; }

    public string NombreLinea { get; set; } = null!;

    public int? SucursalId { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<OrdenProduccion> OrdenProduccions { get; set; } = new List<OrdenProduccion>();

    public virtual Sucursale? Sucursal { get; set; }
}
