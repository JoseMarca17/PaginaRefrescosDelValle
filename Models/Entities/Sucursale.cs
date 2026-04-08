using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Sucursale
{
    public int SucursalId { get; set; }

    public string NombreSucursal { get; set; } = null!;

    public int CiudadId { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Almacen> Almacens { get; set; } = new List<Almacen>();

    public virtual Ciudade Ciudad { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<HistorialCargo> HistorialCargos { get; set; } = new List<HistorialCargo>();

    public virtual ICollection<LineaProduccion> LineaProduccions { get; set; } = new List<LineaProduccion>();

    public virtual ICollection<MedioTransporte> MedioTransportes { get; set; } = new List<MedioTransporte>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<UsuariosSucursale> UsuariosSucursales { get; set; } = new List<UsuariosSucursale>();
}
