using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class UnidadMedidum
{
    public int UnidadMedidaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Abreviatura { get; set; } = null!;

    public string? TipoUnidad { get; set; }

    public virtual ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();

    public virtual ICollection<Presentacione> Presentaciones { get; set; } = new List<Presentacione>();

    public virtual ICollection<RecetaDetalle> RecetaDetalles { get; set; } = new List<RecetaDetalle>();
}
