using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class InstitucionesEducativa
{
    public int InstitucionId { get; set; }

    public string NombreInstitucion { get; set; } = null!;

    public string? Sigla { get; set; }

    public string TipoInstitucion { get; set; } = null!;

    public virtual ICollection<TitulosPersona> TitulosPersonas { get; set; } = new List<TitulosPersona>();
}
