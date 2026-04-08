using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class TelefonosPersona
{
    public int TelefonoPersonaId { get; set; }

    public int PersonaId { get; set; }

    public int TipoContactoId { get; set; }

    public string Numero { get; set; } = null!;

    public string CodigoPais { get; set; } = null!;

    public bool EsPrincipal { get; set; }

    public bool Activo { get; set; }

    public virtual Persona Persona { get; set; } = null!;

    public virtual DominioValor TipoContacto { get; set; } = null!;
}
