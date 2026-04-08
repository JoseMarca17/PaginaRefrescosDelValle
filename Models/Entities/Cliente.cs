using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public int PersonaId { get; set; }

    public int? TipoClienteId { get; set; }

    public decimal? LimiteCredito { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string? Observaciones { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Persona Persona { get; set; } = null!;

    public virtual TiposCliente? TipoCliente { get; set; }
}
