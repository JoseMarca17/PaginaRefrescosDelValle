using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class RecetaDetalle
{
    public int RecetaDetalleId { get; set; }

    public int RecetaId { get; set; }

    public int IngredienteId { get; set; }

    public decimal Cantidad { get; set; }

    public int UnidadMedidaId { get; set; }

    public decimal Merma { get; set; }

    public int? OrdenProceso { get; set; }

    public virtual Ingrediente Ingrediente { get; set; } = null!;

    public virtual Receta Receta { get; set; } = null!;

    public virtual UnidadMedidum UnidadMedida { get; set; } = null!;
}
