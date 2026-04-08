using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class HorasExtra
{
    public int HorasExtraId { get; set; }

    public int EmpleadoId { get; set; }

    public DateOnly Fecha { get; set; }

    public decimal CantidadHoras { get; set; }

    public string? Motivo { get; set; }

    public bool Autorizado { get; set; }

    public int? AutorizadoPorId { get; set; }

    public DateTime? FechaAutorizacion { get; set; }

    public virtual Usuario? AutorizadoPor { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;
}
