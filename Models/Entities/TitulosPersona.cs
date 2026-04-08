using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class TitulosPersona
{
    public int TituloPersonaId { get; set; }

    public int PersonaId { get; set; }

    public int ProfesionId { get; set; }

    public int? EspecialidadId { get; set; }

    public int InstitucionId { get; set; }

    public int NivelEducativoId { get; set; }

    public string TituloObtenido { get; set; } = null!;

    public DateOnly? FechaGraduacion { get; set; }

    public string? NumeroDiploma { get; set; }

    public bool EsPrincipal { get; set; }

    public virtual Especialidade? Especialidad { get; set; }

    public virtual InstitucionesEducativa Institucion { get; set; } = null!;

    public virtual DominioValor NivelEducativo { get; set; } = null!;

    public virtual Persona Persona { get; set; } = null!;

    public virtual Profesione Profesion { get; set; } = null!;
}
