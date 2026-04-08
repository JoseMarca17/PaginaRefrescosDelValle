using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class BitacoraAccione
{
    public int BitacoraId { get; set; }

    public int UsuarioId { get; set; }

    public int? SesionId { get; set; }

    public string Accion { get; set; } = null!;

    public string? Tabla { get; set; }

    public int? RegistroId { get; set; }

    public string? ValorAnterior { get; set; }

    public string? ValorNuevo { get; set; }

    public DateTime FechaAccion { get; set; }

    public string? DireccionIp { get; set; }

    public virtual Sesione? Sesion { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
