using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Presentacione
{
    public int PresentacionId { get; set; }

    public string NombrePresentacion { get; set; } = null!;

    public decimal Capacidad { get; set; }

    public int UnidadMedidaId { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<OrdenProduccion> OrdenProduccions { get; set; } = new List<OrdenProduccion>();

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

    public virtual UnidadMedidum UnidadMedida { get; set; } = null!;
}
