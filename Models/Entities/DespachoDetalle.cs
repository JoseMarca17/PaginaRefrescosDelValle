using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class DespachoDetalle
{
    public int DespachoDetalleId { get; set; }

    public int DespachoId { get; set; }

    public int ContenidoId { get; set; }

    public decimal Cantidad { get; set; }

    public string Destino { get; set; } = null!;

    public virtual Contenido Contenido { get; set; } = null!;

    public virtual Despacho Despacho { get; set; } = null!;
}
