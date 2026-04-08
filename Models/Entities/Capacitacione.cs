using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Capacitacione
{
    public int CapacitacionId { get; set; }

    public int EmpleadoId { get; set; }

    public string NombreCurso { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string? Institucion { get; set; }

    public bool Certificado { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;
}
