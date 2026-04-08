using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class DominioTipo
{
    public byte DominioTipoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<DominioValor> DominioValors { get; set; } = new List<DominioValor>();
}
