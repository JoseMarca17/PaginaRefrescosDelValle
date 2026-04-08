using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Profesione
{
    public int ProfesionId { get; set; }

    public string NombreProfesion { get; set; } = null!;

    public int AreaConocimientoId { get; set; }

    public virtual DominioValor AreaConocimiento { get; set; } = null!;

    public virtual ICollection<Especialidade> Especialidades { get; set; } = new List<Especialidade>();

    public virtual ICollection<TitulosPersona> TitulosPersonas { get; set; } = new List<TitulosPersona>();
}
