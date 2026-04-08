using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class IdiomasPersona
{
    public int IdiomaPersonaId { get; set; }

    public int PersonaId { get; set; }

    public int IdiomaId { get; set; }

    public int NivelDominioId { get; set; }

    public bool EsNativo { get; set; }

    public virtual DominioValor Idioma { get; set; } = null!;

    public virtual DominioValor NivelDominio { get; set; } = null!;

    public virtual Persona Persona { get; set; } = null!;
}
