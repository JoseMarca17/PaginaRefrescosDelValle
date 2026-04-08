using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class DocumentosPersona
{
    public int DocPersonaId { get; set; }

    public int PersonaId { get; set; }

    public int TipoDocumentoId { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string? Extension { get; set; }

    public DateOnly? FechaEmision { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public bool EsPrincipal { get; set; }

    public virtual Persona Persona { get; set; } = null!;

    public virtual DominioValor TipoDocumento { get; set; } = null!;
}
