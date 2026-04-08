using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Baja
{
    public int BajaId { get; set; }

    public int EmpleadoId { get; set; }

    public DateOnly FechaBaja { get; set; }

    public string Motivo { get; set; } = null!;

    public int TipoBajaId { get; set; }

    public int? ProcesadoPorId { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Usuario? ProcesadoPor { get; set; }

    public virtual DominioValor TipoBaja { get; set; } = null!;
}
