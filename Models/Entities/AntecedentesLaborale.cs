using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class AntecedentesLaborale
{
    public int AntecedenteLaboralId { get; set; }

    public int EmpleadoId { get; set; }

    public string Empresa { get; set; } = null!;

    public string CargoOcupado { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public string? MotivoSalida { get; set; }

    public string? Referencia { get; set; }

    public string? TelefonoReferencia { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;
}
