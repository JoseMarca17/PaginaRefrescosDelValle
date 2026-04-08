using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Cargo
{
    public int CargoId { get; set; }

    public string NombreCargo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal SalarioBase { get; set; }

    public int? CargoPadreId { get; set; }

    public bool Activo { get; set; }

    public virtual Cargo? CargoPadre { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<HistorialCargo> HistorialCargos { get; set; } = new List<HistorialCargo>();

    public virtual ICollection<Cargo> InverseCargoPadre { get; set; } = new List<Cargo>();
}
