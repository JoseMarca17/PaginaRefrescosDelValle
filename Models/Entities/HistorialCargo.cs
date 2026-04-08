using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class HistorialCargo
{
    public int HistorialCargoId { get; set; }

    public int EmpleadoId { get; set; }

    public int CargoId { get; set; }

    public int DepartamentoId { get; set; }

    public int SucursalId { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public decimal SalarioEnVigor { get; set; }

    public string? Motivo { get; set; }

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual DepartamentosEmpresa Departamento { get; set; } = null!;

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual Sucursale Sucursal { get; set; } = null!;
}
