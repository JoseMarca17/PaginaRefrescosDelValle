using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class OrdenProduccion
{
    public int OrdenProduccionId { get; set; }

    public string NumeroOrden { get; set; } = null!;

    public int ProductoId { get; set; }

    public int PresentacionId { get; set; }

    public int RecetaId { get; set; }

    public decimal CantidadPlanificada { get; set; }

    public decimal CantidadProducida { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public int EstadoProduccionId { get; set; }

    public int PrioridadId { get; set; }

    public int TurnoId { get; set; }

    public int? LineaId { get; set; }

    public int? ResponsableId { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<ControlCalidadProduccion> ControlCalidadProduccions { get; set; } = new List<ControlCalidadProduccion>();

    public virtual DominioValor EstadoProduccion { get; set; } = null!;

    public virtual LineaProduccion? Linea { get; set; }

    public virtual Presentacione Presentacion { get; set; } = null!;

    public virtual DominioValor Prioridad { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;

    public virtual Receta Receta { get; set; } = null!;

    public virtual Persona? Responsable { get; set; }

    public virtual DominioValor Turno { get; set; } = null!;
}
