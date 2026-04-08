using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? CodigoSku { get; set; }

    public string? UnidadMedidaTexto { get; set; }

    public string? Descripcion { get; set; }

    public int? CategoriaProductoId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual CategoriaProducto? CategoriaProducto { get; set; }

    public virtual ICollection<Contenido> Contenidos { get; set; } = new List<Contenido>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<OrdenProduccion> OrdenProduccions { get; set; } = new List<OrdenProduccion>();

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

    public virtual ICollection<Receta> Receta { get; set; } = new List<Receta>();
}
