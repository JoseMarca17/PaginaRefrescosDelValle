using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class RolesMenu
{
    public int RolMenuId { get; set; }

    public int RolId { get; set; }

    public int MenuId { get; set; }

    public DateTime FechaAsignacion { get; set; }

    public virtual MenuSistema Menu { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;
}
