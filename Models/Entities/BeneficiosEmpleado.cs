using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class BeneficiosEmpleado
{
    public int BeneficioEmpleadoId { get; set; }

    public int EmpleadoId { get; set; }

    public int TipoBeneficioId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal? MontoMensual { get; set; }

    public bool Activo { get; set; }

    public string? Observaciones { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual DominioValor TipoBeneficio { get; set; } = null!;
}
