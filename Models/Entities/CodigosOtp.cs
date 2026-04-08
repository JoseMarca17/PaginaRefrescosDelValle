using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class CodigosOtp
{
    public int IdCodigo { get; set; }

    public int IdUsuario { get; set; }

    public string Codigo { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime FechaExpiracion { get; set; }

    public bool Usado { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
