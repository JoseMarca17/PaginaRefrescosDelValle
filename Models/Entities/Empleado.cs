using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Empleado
{
    public int EmpleadoId { get; set; }

    public int PersonaId { get; set; }

    public int CargoId { get; set; }

    public int DepartamentoId { get; set; }

    public int SucursalId { get; set; }

    public int? SupervisorId { get; set; }

    public int EstadoEmpleadoId { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public decimal Salario { get; set; }

    public virtual ICollection<AntecedentesAcademico> AntecedentesAcademicos { get; set; } = new List<AntecedentesAcademico>();

    public virtual ICollection<AntecedentesLaborale> AntecedentesLaborales { get; set; } = new List<AntecedentesLaborale>();

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<Baja> Bajas { get; set; } = new List<Baja>();

    public virtual ICollection<BeneficiosEmpleado> BeneficiosEmpleados { get; set; } = new List<BeneficiosEmpleado>();

    public virtual ICollection<Capacitacione> Capacitaciones { get; set; } = new List<Capacitacione>();

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual ICollection<ContratosEmpleado> ContratosEmpleados { get; set; } = new List<ContratosEmpleado>();

    public virtual DepartamentosEmpresa Departamento { get; set; } = null!;

    public virtual ICollection<DepartamentosEmpresa> DepartamentosEmpresas { get; set; } = new List<DepartamentosEmpresa>();

    public virtual DominioValor EstadoEmpleado { get; set; } = null!;

    public virtual ICollection<HistorialCargo> HistorialCargos { get; set; } = new List<HistorialCargo>();

    public virtual ICollection<HorasExtra> HorasExtras { get; set; } = new List<HorasExtra>();

    public virtual ICollection<Empleado> InverseSupervisor { get; set; } = new List<Empleado>();

    public virtual Persona Persona { get; set; } = null!;

    public virtual ICollection<Planilla> Planillas { get; set; } = new List<Planilla>();

    public virtual ICollection<SaldoVacacione> SaldoVacaciones { get; set; } = new List<SaldoVacacione>();

    public virtual ICollection<Sancione> Sanciones { get; set; } = new List<Sancione>();

    public virtual Sucursale Sucursal { get; set; } = null!;

    public virtual Empleado? Supervisor { get; set; }

    public virtual ICollection<Vacacione> Vacaciones { get; set; } = new List<Vacacione>();
}
