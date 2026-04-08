using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Planilla
{
    public int PlanillaId { get; set; }

    public int EmpleadoId { get; set; }

    public int Mes { get; set; }

    public int Anio { get; set; }

    public decimal HaberBasico { get; set; }

    public decimal Bonos { get; set; }

    public decimal Descuentos { get; set; }

    public decimal? TotalLiquido { get; set; }

    public DateOnly? FechaPago { get; set; }

    public bool Pagado { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual PlanillaDetalle? PlanillaDetalle { get; set; }
}
