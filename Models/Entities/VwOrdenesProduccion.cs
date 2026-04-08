using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class VwOrdenesProduccion
{
    public int OrdenProduccionId { get; set; }

    public string NumeroOrden { get; set; } = null!;

    public string NombreProducto { get; set; } = null!;

    public string NombrePresentacion { get; set; } = null!;

    public string VersionReceta { get; set; } = null!;

    public decimal CantidadPlanificada { get; set; }

    public decimal CantidadProducida { get; set; }

    public string EstadoProduccion { get; set; } = null!;

    public string Prioridad { get; set; } = null!;

    public string Turno { get; set; } = null!;

    public string? NombreLinea { get; set; }

    public string Responsable { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }
}
