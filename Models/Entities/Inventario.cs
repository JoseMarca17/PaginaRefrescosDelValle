using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Inventario
{
    public int InventarioId { get; set; }

    public int ProductoId { get; set; }

    public string? Lote { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public decimal CostoUnitario { get; set; }

    public decimal? PrecioVenta { get; set; }

    public string? ProveedorRef { get; set; }

    public string? Observaciones { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Contenido> Contenidos { get; set; } = new List<Contenido>();

    public virtual Producto Producto { get; set; } = null!;

    public virtual ICollection<RecepcionMercaderium> RecepcionMercaderia { get; set; } = new List<RecepcionMercaderium>();
}
