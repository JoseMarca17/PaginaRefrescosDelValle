using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Zona
{
    public int ZonaId { get; set; }

    public string NombreZona { get; set; } = null!;

    public int CiudadId { get; set; }

    public virtual Ciudade Ciudad { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
