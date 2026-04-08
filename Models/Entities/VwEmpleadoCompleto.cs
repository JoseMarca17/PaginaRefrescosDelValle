using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class VwEmpleadoCompleto
{
    public int EmpleadoId { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public string NombreCargo { get; set; } = null!;

    public string NombreDepartamento { get; set; } = null!;

    public string NombreSucursal { get; set; } = null!;

    public string EstadoEmpleado { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public decimal Salario { get; set; }

    public string NombreSupervisor { get; set; } = null!;
}
