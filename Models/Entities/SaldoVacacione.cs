using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class SaldoVacacione
{
    public int SaldoVacacionId { get; set; }

    public int EmpleadoId { get; set; }

    public int Anio { get; set; }

    public int DiasCorresponde { get; set; }

    public int DiasUsados { get; set; }

    public int? DiasRestantes { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;
}
