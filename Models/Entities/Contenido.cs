using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Contenido
{
    public int ContenidoId { get; set; }

    public int AlmacenId { get; set; }

    public int ProductoId { get; set; }

    public int InventarioId { get; set; }

    public decimal CantidadDisponible { get; set; }

    public decimal CantidadMinima { get; set; }

    public int EstadoContenidoId { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public virtual Almacen Almacen { get; set; } = null!;

    public virtual ICollection<DespachoDetalle> DespachoDetalles { get; set; } = new List<DespachoDetalle>();

    public virtual DominioValor EstadoContenido { get; set; } = null!;

    public virtual Inventario Inventario { get; set; } = null!;

    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();

    public virtual ICollection<MovimientoDetalle> MovimientoDetalles { get; set; } = new List<MovimientoDetalle>();

    public virtual Producto Producto { get; set; } = null!;
}
