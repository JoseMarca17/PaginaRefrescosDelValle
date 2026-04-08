using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class Persona
{
    public int PersonaId { get; set; }

    public string Nombres { get; set; } = null!;

    public string ApellidoPat { get; set; } = null!;

    public string? ApellidoMat { get; set; }

    public int TipoDocumentoId { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public string? NumeroDocExtension { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public int SexoId { get; set; }

    public int? TipoSangreId { get; set; }

    public int? EstadoCivilId { get; set; }

    public string? TelefonoPrincipal { get; set; }

    public string? CorreoPrincipal { get; set; }

    public string? DireccionLinea1 { get; set; }

    public string? DireccionLinea2 { get; set; }

    public int? ZonaId { get; set; }

    public int? CiudadResidenciaId { get; set; }

    public string? FotoUrl { get; set; }

    public string? Observaciones { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Ciudade? CiudadResidencia { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual ICollection<ControlCalidadProduccion> ControlCalidadProduccions { get; set; } = new List<ControlCalidadProduccion>();

    public virtual ICollection<Despacho> Despachos { get; set; } = new List<Despacho>();

    public virtual ICollection<DiscapacidadesPersona> DiscapacidadesPersonas { get; set; } = new List<DiscapacidadesPersona>();

    public virtual ICollection<DocumentosPersona> DocumentosPersonas { get; set; } = new List<DocumentosPersona>();

    public virtual ICollection<EmailsPersona> EmailsPersonas { get; set; } = new List<EmailsPersona>();

    public virtual Empleado? Empleado { get; set; }

    public virtual DominioValor? EstadoCivil { get; set; }

    public virtual ICollection<IdiomasPersona> IdiomasPersonas { get; set; } = new List<IdiomasPersona>();

    public virtual ICollection<OrdenProduccion> OrdenProduccions { get; set; } = new List<OrdenProduccion>();

    public virtual Proveedore? Proveedore { get; set; }

    public virtual DominioValor Sexo { get; set; } = null!;

    public virtual ICollection<TelefonosPersona> TelefonosPersonas { get; set; } = new List<TelefonosPersona>();

    public virtual DominioValor TipoDocumento { get; set; } = null!;

    public virtual DominioValor? TipoSangre { get; set; }

    public virtual ICollection<TitulosPersona> TitulosPersonas { get; set; } = new List<TitulosPersona>();

    public virtual Usuario? Usuario { get; set; }

    public virtual Zona? Zona { get; set; }
}
