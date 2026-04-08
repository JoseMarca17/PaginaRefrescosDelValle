using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Vacacione
{
    public int VacacionId { get; set; }

    public int EmpleadoId { get; set; }

    public DateTime FechaSolicitud { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public int DiasSolicitados { get; set; }

    public int EstadoVacacionId { get; set; }

    public int? AprobadoPorId { get; set; }

    public string? Observaciones { get; set; }

    public virtual Usuario? AprobadoPor { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual DominioValor EstadoVacacion { get; set; } = null!;
}
