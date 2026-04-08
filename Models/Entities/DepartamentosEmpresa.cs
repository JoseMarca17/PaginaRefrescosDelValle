using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class DepartamentosEmpresa
{
    public int DepartamentoId { get; set; }

    public string NombreDepartamento { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? DepartamentoPadreId { get; set; }

    public int? JefeEmpleadoId { get; set; }

    public bool Activo { get; set; }

    public virtual DepartamentosEmpresa? DepartamentoPadre { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<HistorialCargo> HistorialCargos { get; set; } = new List<HistorialCargo>();

    public virtual ICollection<DepartamentosEmpresa> InverseDepartamentoPadre { get; set; } = new List<DepartamentosEmpresa>();

    public virtual Empleado? JefeEmpleado { get; set; }
}
