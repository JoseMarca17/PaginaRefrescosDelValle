using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Sesione
{
    public int SesionId { get; set; }

    public int UsuarioId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaCierre { get; set; }

    public string? DireccionIp { get; set; }

    public string? Dispositivo { get; set; }

    public bool Activa { get; set; }

    public virtual ICollection<BitacoraAccione> BitacoraAcciones { get; set; } = new List<BitacoraAccione>();

    public virtual Usuario Usuario { get; set; } = null!;
}
