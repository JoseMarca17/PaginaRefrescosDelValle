using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Movimiento
{
    public int MovimientoId { get; set; }

    public int TipoMovimientoId { get; set; }

    public int? MedioTransporteId { get; set; }

    public int MovimientoDetalleId { get; set; }

    public DateOnly FechaMovimiento { get; set; }

    public string? Referencia { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int? UsuarioRegistroId { get; set; }

    public virtual MedioTransporte? MedioTransporte { get; set; }

    public virtual MovimientoDetalle MovimientoDetalle { get; set; } = null!;

    public virtual DominioValor TipoMovimiento { get; set; } = null!;

    public virtual Usuario? UsuarioRegistro { get; set; }
}
