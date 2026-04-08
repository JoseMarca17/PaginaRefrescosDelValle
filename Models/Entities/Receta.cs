using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Receta
{
    public int RecetaId { get; set; }

    public int ProductoId { get; set; }

    public string Version { get; set; } = null!;

    public int TipoRecetaId { get; set; }

    public int EstadoRecetaId { get; set; }

    public bool EsConfidencial { get; set; }

    public bool EsActiva { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaAprobacion { get; set; }

    public int? UsuarioCreacionId { get; set; }

    public int? UsuarioAprobacionId { get; set; }

    public string? Observaciones { get; set; }

    public virtual DominioValor EstadoReceta { get; set; } = null!;

    public virtual ICollection<OrdenProduccion> OrdenProduccions { get; set; } = new List<OrdenProduccion>();

    public virtual Producto Producto { get; set; } = null!;

    public virtual ICollection<RecetaDetalle> RecetaDetalles { get; set; } = new List<RecetaDetalle>();

    public virtual DominioValor TipoReceta { get; set; } = null!;

    public virtual Usuario? UsuarioAprobacion { get; set; }

    public virtual Usuario? UsuarioCreacion { get; set; }
}
