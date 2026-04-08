using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class CategoriaIngrediente
{
    public int CategoriaIngredienteId { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
}
