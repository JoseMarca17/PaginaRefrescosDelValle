using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class CategoriaProducto
{
    public int CategoriaProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
