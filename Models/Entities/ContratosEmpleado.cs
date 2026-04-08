using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class ContratosEmpleado
{
    public int ContratoEmpleadoId { get; set; }

    public int EmpleadoId { get; set; }

    public int TipoContratoId { get; set; }

    public int EstadoContratoId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal Salario { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual DominioValor EstadoContrato { get; set; } = null!;

    public virtual DominioValor TipoContrato { get; set; } = null!;
}
