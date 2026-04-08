using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Merma
{
    public int MermaId { get; set; }

    public int ContenidoId { get; set; }

    public int TipoMermaId { get; set; }

    public decimal CantidadPerdida { get; set; }

    public DateOnly FechaMerma { get; set; }

    public string? Causa { get; set; }

    public int? UsuarioRegistroId { get; set; }

    public string? Observaciones { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Contenido Contenido { get; set; } = null!;

    public virtual DominioValor TipoMerma { get; set; } = null!;

    public virtual Usuario? UsuarioRegistro { get; set; }
}
