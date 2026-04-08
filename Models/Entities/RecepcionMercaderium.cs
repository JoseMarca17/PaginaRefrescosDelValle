using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class RecepcionMercaderium
{
    public int RecepcionId { get; set; }

    public int OrdenCompraId { get; set; }

    public int InventarioId { get; set; }

    public DateOnly FechaRecepcion { get; set; }

    public decimal CantidadRecibida { get; set; }

    public int AlmacenId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Observaciones { get; set; }

    public virtual Almacen Almacen { get; set; } = null!;

    public virtual Inventario Inventario { get; set; } = null!;

    public virtual OrdenesCompra OrdenCompra { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
