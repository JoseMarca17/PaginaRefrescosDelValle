using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Almacen
{
    public int AlmacenId { get; set; }

    public string NombreAlmacen { get; set; } = null!;

    public int TipoAlmacenId { get; set; }

    public int SucursalId { get; set; }

    public int EstadoAlmacenId { get; set; }

    public string? Direccion { get; set; }

    public string? Observaciones { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Contenido> Contenidos { get; set; } = new List<Contenido>();

    public virtual DominioValor EstadoAlmacen { get; set; } = null!;

    public virtual ICollection<RecepcionMercaderium> RecepcionMercaderia { get; set; } = new List<RecepcionMercaderium>();

    public virtual Sucursale Sucursal { get; set; } = null!;

    public virtual DominioValor TipoAlmacen { get; set; } = null!;
}
