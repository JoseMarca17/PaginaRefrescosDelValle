using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Permiso
{
    public int PermisoId { get; set; }

    public string NombrePermiso { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Modulo { get; set; } = null!;

    public string Accion { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<RolesPermiso> RolesPermisos { get; set; } = new List<RolesPermiso>();
}
