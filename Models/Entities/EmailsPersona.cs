using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class EmailsPersona
{
    public int EmailPersonaId { get; set; }

    public int PersonaId { get; set; }

    public int TipoContactoId { get; set; }

    public string Correo { get; set; } = null!;

    public bool EsPrincipal { get; set; }

    public bool Activo { get; set; }

    public virtual Persona Persona { get; set; } = null!;

    public virtual DominioValor TipoContacto { get; set; } = null!;
}
