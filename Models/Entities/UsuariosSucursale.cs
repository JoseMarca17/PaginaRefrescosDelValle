using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class UsuariosSucursale
{
    public int UsuarioSucursalId { get; set; }

    public int UsuarioId { get; set; }

    public int SucursalId { get; set; }

    public bool EsPrincipal { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public virtual Sucursale Sucursal { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
