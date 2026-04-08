using System;
using System.Collections.Generic;

namespace RefrescosDelValle.Models.Entities;

public partial class DominioValor
{
    public int DominioValorId { get; set; }

    public byte DominioTipoId { get; set; }

    public string Descripcion { get; set; } = null!;

    public byte Orden { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Almacen> AlmacenEstadoAlmacens { get; set; } = new List<Almacen>();

    public virtual ICollection<Almacen> AlmacenTipoAlmacens { get; set; } = new List<Almacen>();

    public virtual ICollection<AntecedentesAcademico> AntecedentesAcademicos { get; set; } = new List<AntecedentesAcademico>();

    public virtual ICollection<Asistencium> Asistencia { get; set; } = new List<Asistencium>();

    public virtual ICollection<Baja> Bajas { get; set; } = new List<Baja>();

    public virtual ICollection<BeneficiosEmpleado> BeneficiosEmpleados { get; set; } = new List<BeneficiosEmpleado>();

    public virtual ICollection<Contenido> Contenidos { get; set; } = new List<Contenido>();

    public virtual ICollection<ContratosEmpleado> ContratosEmpleadoEstadoContratos { get; set; } = new List<ContratosEmpleado>();

    public virtual ICollection<ContratosEmpleado> ContratosEmpleadoTipoContratos { get; set; } = new List<ContratosEmpleado>();

    public virtual ICollection<DiscapacidadesPersona> DiscapacidadesPersonas { get; set; } = new List<DiscapacidadesPersona>();

    public virtual ICollection<DocumentosPersona> DocumentosPersonas { get; set; } = new List<DocumentosPersona>();

    public virtual DominioTipo DominioTipo { get; set; } = null!;

    public virtual ICollection<EmailsPersona> EmailsPersonas { get; set; } = new List<EmailsPersona>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<IdiomasPersona> IdiomasPersonaIdiomas { get; set; } = new List<IdiomasPersona>();

    public virtual ICollection<IdiomasPersona> IdiomasPersonaNivelDominios { get; set; } = new List<IdiomasPersona>();

    public virtual ICollection<MedioTransporte> MedioTransportes { get; set; } = new List<MedioTransporte>();

    public virtual ICollection<Merma> Mermas { get; set; } = new List<Merma>();

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual ICollection<OrdenProduccion> OrdenProduccionEstadoProduccions { get; set; } = new List<OrdenProduccion>();

    public virtual ICollection<OrdenProduccion> OrdenProduccionPrioridads { get; set; } = new List<OrdenProduccion>();

    public virtual ICollection<OrdenProduccion> OrdenProduccionTurnos { get; set; } = new List<OrdenProduccion>();

    public virtual ICollection<Persona> PersonaEstadoCivils { get; set; } = new List<Persona>();

    public virtual ICollection<Persona> PersonaSexos { get; set; } = new List<Persona>();

    public virtual ICollection<Persona> PersonaTipoDocumentos { get; set; } = new List<Persona>();

    public virtual ICollection<Persona> PersonaTipoSangres { get; set; } = new List<Persona>();

    public virtual ICollection<Profesione> Profesiones { get; set; } = new List<Profesione>();

    public virtual ICollection<Receta> RecetaEstadoReceta { get; set; } = new List<Receta>();

    public virtual ICollection<Receta> RecetaTipoReceta { get; set; } = new List<Receta>();

    public virtual ICollection<Sancione> Sanciones { get; set; } = new List<Sancione>();

    public virtual ICollection<TelefonosPersona> TelefonosPersonas { get; set; } = new List<TelefonosPersona>();

    public virtual ICollection<TitulosPersona> TitulosPersonas { get; set; } = new List<TitulosPersona>();

    public virtual ICollection<Vacacione> Vacaciones { get; set; } = new List<Vacacione>();
}
