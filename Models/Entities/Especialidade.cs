using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Especialidade
{
    public int EspecialidadId { get; set; }

    public int ProfesionId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual Profesione Profesion { get; set; } = null!;

    public virtual ICollection<TitulosPersona> TitulosPersonas { get; set; } = new List<TitulosPersona>();
}
