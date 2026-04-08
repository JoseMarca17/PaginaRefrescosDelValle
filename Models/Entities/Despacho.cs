using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Despacho
{
    public int DespachoId { get; set; }

    public DateOnly FechaDespacho { get; set; }

    public int MedioTransporteId { get; set; }

    public int? ResponsableId { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<DespachoDetalle> DespachoDetalles { get; set; } = new List<DespachoDetalle>();

    public virtual MedioTransporte MedioTransporte { get; set; } = null!;

    public virtual Persona? Responsable { get; set; }
}
