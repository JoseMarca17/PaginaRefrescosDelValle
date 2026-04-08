using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Sancione
{
    public int SancionId { get; set; }

    public int EmpleadoId { get; set; }

    public int TipoSancionId { get; set; }

    public DateOnly FechaSancion { get; set; }

    public string Motivo { get; set; } = null!;

    public int AplicadaPorId { get; set; }

    public string? Observaciones { get; set; }

    public virtual Usuario AplicadaPor { get; set; } = null!;

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual DominioValor TipoSancion { get; set; } = null!;
}
