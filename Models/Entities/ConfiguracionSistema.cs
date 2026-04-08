using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class ConfiguracionSistema
{
    public int ConfigId { get; set; }

    public string Clave { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Tipo { get; set; } = null!;

    public DateTime FechaModificacion { get; set; }

    public int? UsuarioModifico { get; set; }

    public virtual Usuario? UsuarioModificoNavigation { get; set; }
}
