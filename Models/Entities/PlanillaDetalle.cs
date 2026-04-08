using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class PlanillaDetalle
{
    public int DetalleId { get; set; }

    public int PlanillaId { get; set; }

    public decimal Afp { get; set; }

    public decimal RcIva { get; set; }

    public decimal Cns { get; set; }

    public virtual Planilla Planilla { get; set; } = null!;
}
