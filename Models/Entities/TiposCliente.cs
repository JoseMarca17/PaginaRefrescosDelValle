using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class TiposCliente
{
    public int TipoClienteId { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
