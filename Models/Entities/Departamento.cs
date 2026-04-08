using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Departamento
{
    public int DepartamentoGeoId { get; set; }

    public string NombreDpto { get; set; } = null!;

    public virtual ICollection<Ciudade> Ciudades { get; set; } = new List<Ciudade>();
}
