using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class VwStockAlmacen
{
    public int AlmacenId { get; set; }

    public string NombreAlmacen { get; set; } = null!;

    public string TipoAlmacen { get; set; } = null!;

    public string EstadoAlmacen { get; set; } = null!;

    public string NombreSucursal { get; set; } = null!;

    public int ProductoId { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? CodigoSku { get; set; }

    public string? Lote { get; set; }

    public DateOnly FechaIngreso { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public decimal CostoUnitario { get; set; }

    public decimal CantidadDisponible { get; set; }

    public decimal CantidadMinima { get; set; }

    public int BajoMinimo { get; set; }

    public string EstadoContenido { get; set; } = null!;

    public DateTime FechaActualizacion { get; set; }
}
