using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class VwMerma
{
    public int MermaId { get; set; }

    public DateOnly FechaMerma { get; set; }

    public string TipoMerma { get; set; } = null!;

    public string? Causa { get; set; }

    public decimal CantidadPerdida { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string? CodigoSku { get; set; }

    public string? Lote { get; set; }

    public DateOnly? FechaVencimiento { get; set; }

    public string NombreAlmacen { get; set; } = null!;

    public string NombreSucursal { get; set; } = null!;

    public string? Observaciones { get; set; }
}
