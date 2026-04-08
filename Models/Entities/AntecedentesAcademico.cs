using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class AntecedentesAcademico
{
    public int AntecedenteAcademicoId { get; set; }

    public int EmpleadoId { get; set; }

    public int NivelEducacionId { get; set; }

    public string Institucion { get; set; } = null!;

    public string? TituloObtenido { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly? FechaGraduacion { get; set; }

    public bool Graduado { get; set; }

    public string? Observaciones { get; set; }

    public virtual Empleado Empleado { get; set; } = null!;

    public virtual DominioValor NivelEducacion { get; set; } = null!;
}
