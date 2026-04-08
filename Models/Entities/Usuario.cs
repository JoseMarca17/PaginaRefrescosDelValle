using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public int PersonaId { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public virtual ICollection<Baja> Bajas { get; set; } = new List<Baja>();

    public virtual ICollection<BitacoraAccione> BitacoraAcciones { get; set; } = new List<BitacoraAccione>();

    public virtual ICollection<CodigosOtp> CodigosOtps { get; set; } = new List<CodigosOtp>();

    public virtual ICollection<ConfiguracionSistema> ConfiguracionSistemas { get; set; } = new List<ConfiguracionSistema>();

    public virtual ICollection<HorasExtra> HorasExtras { get; set; } = new List<HorasExtra>();

    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual ICollection<OrdenesCompra> OrdenesCompras { get; set; } = new List<OrdenesCompra>();

    public virtual ICollection<PagosCliente> PagosClientes { get; set; } = new List<PagosCliente>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Persona Persona { get; set; } = null!;

    public virtual ICollection<RecepcionMercaderium> RecepcionMercaderia { get; set; } = new List<RecepcionMercaderium>();

    public virtual ICollection<Receta> RecetaUsuarioAprobacions { get; set; } = new List<Receta>();

    public virtual ICollection<Receta> RecetaUsuarioCreacions { get; set; } = new List<Receta>();

    public virtual ICollection<Sancione> Sanciones { get; set; } = new List<Sancione>();

    public virtual ICollection<Sesione> Sesiones { get; set; } = new List<Sesione>();

    public virtual ICollection<UsuariosRole> UsuariosRoles { get; set; } = new List<UsuariosRole>();

    public virtual ICollection<UsuariosSucursale> UsuariosSucursales { get; set; } = new List<UsuariosSucursale>();

    public virtual ICollection<Vacacione> Vacaciones { get; set; } = new List<Vacacione>();
}
