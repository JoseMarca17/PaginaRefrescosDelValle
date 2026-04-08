using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class DiscapacidadesPersona
{
    public int DiscapacidadPersonaId { get; set; }

    public int PersonaId { get; set; }

    public int TipoDiscapacidadId { get; set; }

    public decimal? GradoPorcentaje { get; set; }

    public string? NumeroCarnet { get; set; }

    public DateOnly FechaRegistro { get; set; }

    public string? Observaciones { get; set; }

    public virtual Persona Persona { get; set; } = null!;

    public virtual DominioValor TipoDiscapacidad { get; set; } = null!;
}
