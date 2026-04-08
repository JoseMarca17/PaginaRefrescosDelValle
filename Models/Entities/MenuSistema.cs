using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class MenuSistema
{
    public int MenuId { get; set; }

    public string NombreMenu { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Icono { get; set; }

    public string? Ruta { get; set; }

    public int? MenuPadreId { get; set; }

    public int Orden { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<MenuSistema> InverseMenuPadre { get; set; } = new List<MenuSistema>();

    public virtual MenuSistema? MenuPadre { get; set; }

    public virtual ICollection<RolesMenu> RolesMenus { get; set; } = new List<RolesMenu>();
}
