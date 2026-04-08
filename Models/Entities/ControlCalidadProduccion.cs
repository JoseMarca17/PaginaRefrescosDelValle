using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class ControlCalidadProduccion
{
    public int ControlId { get; set; }

    public int OrdenProduccionId { get; set; }

    public DateTime FechaControl { get; set; }

    public int? InspectorId { get; set; }

    public bool? Resultado { get; set; }

    public string? Observaciones { get; set; }

    public virtual Persona? Inspector { get; set; }

    public virtual OrdenProduccion OrdenProduccion { get; set; } = null!;
}
