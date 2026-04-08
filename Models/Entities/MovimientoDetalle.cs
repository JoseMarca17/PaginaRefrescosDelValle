using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class MovimientoDetalle
{
    public int MovimientoDetalleId { get; set; }

    public int ContenidoId { get; set; }

    public DateOnly FechaEnvio { get; set; }

    public DateOnly? FechaRecepcion { get; set; }

    public string Origen { get; set; } = null!;

    public string Destino { get; set; } = null!;

    public decimal Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public virtual Contenido Contenido { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
