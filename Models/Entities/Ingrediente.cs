using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Ingrediente
{
    public int IngredienteId { get; set; }

    public string NombreIngrediente { get; set; } = null!;

    public int? CategoriaIngredienteId { get; set; }

    public int UnidadBaseId { get; set; }

    public virtual CategoriaIngrediente? CategoriaIngrediente { get; set; }

    public virtual ICollection<RecetaDetalle> RecetaDetalles { get; set; } = new List<RecetaDetalle>();

    public virtual UnidadMedidum UnidadBase { get; set; } = null!;
}
