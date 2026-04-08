using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Ciudade
{
    public int CiudadId { get; set; }

    public string NombreCiudad { get; set; } = null!;

    public int DepartamentoGeoId { get; set; }

    public virtual Departamento DepartamentoGeo { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();

    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();

    public virtual ICollection<Zona> Zonas { get; set; } = new List<Zona>();
}
