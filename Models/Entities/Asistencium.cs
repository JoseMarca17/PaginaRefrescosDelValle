using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Asistencium
{
    public int AsistenciaId { get; set; }

    public int EmpleadoId { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    public int EstadoAsistenciaId { get; set; }

    public string? Observaciones { get; set; }

    public bool Justificado { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual DominioValor EstadoAsistencia { get; set; } = null!;
}
