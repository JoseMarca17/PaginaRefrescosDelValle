using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class PagosCliente
{
    public int PagoClienteId { get; set; }

    public int FacturaId { get; set; }

    public DateOnly FechaPago { get; set; }

    public decimal Monto { get; set; }

    public string MedioPago { get; set; } = null!;

    public string? Referencia { get; set; }

    public int? UsuarioId { get; set; }

    public string? Observaciones { get; set; }

    public virtual Factura Factura { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
