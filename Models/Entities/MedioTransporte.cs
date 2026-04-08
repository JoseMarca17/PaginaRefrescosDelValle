using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class MedioTransporte
{
    public int MedioTransporteId { get; set; }

    public string TipoVehiculo { get; set; } = null!;

    public string Placa { get; set; } = null!;

    public string? Conductor { get; set; }

    public int EstadoTransporteId { get; set; }

    public int? SucursalId { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<Despacho> Despachos { get; set; } = new List<Despacho>();

    public virtual DominioValor EstadoTransporte { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual Sucursale? Sucursal { get; set; }
}
