using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RefrescosDelValle.Models.Entities;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Almacen> Almacens { get; set; }

    public virtual DbSet<AntecedentesAcademico> AntecedentesAcademicos { get; set; }

    public virtual DbSet<AntecedentesLaborale> AntecedentesLaborales { get; set; }

    public virtual DbSet<Asistencium> Asistencia { get; set; }

    public virtual DbSet<Baja> Bajas { get; set; }

    public virtual DbSet<BeneficiosEmpleado> BeneficiosEmpleados { get; set; }

    public virtual DbSet<BitacoraAccione> BitacoraAcciones { get; set; }

    public virtual DbSet<Capacitacione> Capacitaciones { get; set; }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<CategoriaIngrediente> CategoriaIngredientes { get; set; }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CodigosOtp> CodigosOtps { get; set; }

    public virtual DbSet<ConfiguracionSistema> ConfiguracionSistemas { get; set; }

    public virtual DbSet<Contenido> Contenidos { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<ContratosEmpleado> ContratosEmpleados { get; set; }

    public virtual DbSet<ControlCalidadProduccion> ControlCalidadProduccions { get; set; }

    public virtual DbSet<CuentasPagar> CuentasPagars { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DepartamentosEmpresa> DepartamentosEmpresas { get; set; }

    public virtual DbSet<Despacho> Despachos { get; set; }

    public virtual DbSet<DespachoDetalle> DespachoDetalles { get; set; }

    public virtual DbSet<DiscapacidadesPersona> DiscapacidadesPersonas { get; set; }

    public virtual DbSet<DocumentosPersona> DocumentosPersonas { get; set; }

    public virtual DbSet<DominioTipo> DominioTipos { get; set; }

    public virtual DbSet<DominioValor> DominioValors { get; set; }

    public virtual DbSet<EmailsPersona> EmailsPersonas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }

    public virtual DbSet<HistorialCargo> HistorialCargos { get; set; }

    public virtual DbSet<HorasExtra> HorasExtras { get; set; }

    public virtual DbSet<IdiomasPersona> IdiomasPersonas { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<InstitucionesEducativa> InstitucionesEducativas { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<LineaProduccion> LineaProduccions { get; set; }

    public virtual DbSet<MedioTransporte> MedioTransportes { get; set; }

    public virtual DbSet<MenuSistema> MenuSistemas { get; set; }

    public virtual DbSet<Merma> Mermas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<MovimientoDetalle> MovimientoDetalles { get; set; }

    public virtual DbSet<OrdenProduccion> OrdenProduccions { get; set; }

    public virtual DbSet<OrdenesCompra> OrdenesCompras { get; set; }

    public virtual DbSet<PagosCliente> PagosClientes { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoDetalle> PedidoDetalles { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Planilla> Planillas { get; set; }

    public virtual DbSet<PlanillaDetalle> PlanillaDetalles { get; set; }

    public virtual DbSet<Presentacione> Presentaciones { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Profesione> Profesiones { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<RecepcionMercaderium> RecepcionMercaderia { get; set; }

    public virtual DbSet<Receta> Recetas { get; set; }

    public virtual DbSet<RecetaDetalle> RecetaDetalles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesMenu> RolesMenus { get; set; }

    public virtual DbSet<RolesPermiso> RolesPermisos { get; set; }

    public virtual DbSet<SaldoVacacione> SaldoVacaciones { get; set; }

    public virtual DbSet<Sancione> Sanciones { get; set; }

    public virtual DbSet<Sesione> Sesiones { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<TelefonosPersona> TelefonosPersonas { get; set; }

    public virtual DbSet<TiposCliente> TiposClientes { get; set; }

    public virtual DbSet<TitulosPersona> TitulosPersonas { get; set; }

    public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; }

    public virtual DbSet<UsuariosSucursale> UsuariosSucursales { get; set; }

    public virtual DbSet<Vacacione> Vacaciones { get; set; }

    public virtual DbSet<VwEmpleadoCompleto> VwEmpleadoCompletos { get; set; }

    public virtual DbSet<VwMerma> VwMermas { get; set; }

    public virtual DbSet<VwOrdenesProduccion> VwOrdenesProduccions { get; set; }

    public virtual DbSet<VwPedidosCompleto> VwPedidosCompletos { get; set; }

    public virtual DbSet<VwStockAlmacen> VwStockAlmacens { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=RefrescosDelValleV1.11;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Almacen>(entity =>
        {
            entity.ToTable("Almacen");

            entity.HasIndex(e => e.EstadoAlmacenId, "IX_Almacen_EstadoAlmacen");

            entity.HasIndex(e => e.SucursalId, "IX_Almacen_SucursalID");

            entity.HasIndex(e => new { e.NombreAlmacen, e.SucursalId }, "UQ_Almacen_Nombre").IsUnique();

            entity.Property(e => e.AlmacenId).HasColumnName("AlmacenID");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.EstadoAlmacenId).HasColumnName("EstadoAlmacenID");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_Almacen_FechaCreacion")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.NombreAlmacen)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");
            entity.Property(e => e.TipoAlmacenId).HasColumnName("TipoAlmacenID");

            entity.HasOne(d => d.EstadoAlmacen).WithMany(p => p.AlmacenEstadoAlmacens)
                .HasForeignKey(d => d.EstadoAlmacenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Almacen_Estado");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Almacens)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Almacen_Sucursal");

            entity.HasOne(d => d.TipoAlmacen).WithMany(p => p.AlmacenTipoAlmacens)
                .HasForeignKey(d => d.TipoAlmacenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Almacen_TipoAlmacen");
        });

        modelBuilder.Entity<AntecedentesAcademico>(entity =>
        {
            entity.HasKey(e => e.AntecedenteAcademicoId);

            entity.Property(e => e.AntecedenteAcademicoId).HasColumnName("AntecedenteAcademicoID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Institucion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NivelEducacionId).HasColumnName("NivelEducacionID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.TituloObtenido)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Empleado).WithMany(p => p.AntecedentesAcademicos)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AntAcad_Empleado");

            entity.HasOne(d => d.NivelEducacion).WithMany(p => p.AntecedentesAcademicos)
                .HasForeignKey(d => d.NivelEducacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AntAcad_NivelEducacion");
        });

        modelBuilder.Entity<AntecedentesLaborale>(entity =>
        {
            entity.HasKey(e => e.AntecedenteLaboralId);

            entity.Property(e => e.AntecedenteLaboralId).HasColumnName("AntecedenteLaboralID");
            entity.Property(e => e.CargoOcupado)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Empresa)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MotivoSalida)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Referencia)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TelefonoReferencia)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Empleado).WithMany(p => p.AntecedentesLaborales)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AntLab_Empleado");
        });

        modelBuilder.Entity<Asistencium>(entity =>
        {
            entity.HasKey(e => e.AsistenciaId);

            entity.HasIndex(e => new { e.EmpleadoId, e.Fecha }, "IX_Asistencia_EmpleadoFecha");

            entity.HasIndex(e => new { e.EmpleadoId, e.Fecha }, "UQ_Asistencia_EmpleadoFecha").IsUnique();

            entity.Property(e => e.AsistenciaId).HasColumnName("AsistenciaID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.EstadoAsistenciaId).HasColumnName("EstadoAsistenciaID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Empleado).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asistencia_Empleado");

            entity.HasOne(d => d.EstadoAsistencia).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.EstadoAsistenciaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asistencia_Estado");
        });

        modelBuilder.Entity<Baja>(entity =>
        {
            entity.Property(e => e.BajaId).HasColumnName("BajaID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProcesadoPorId).HasColumnName("ProcesadoPorID");
            entity.Property(e => e.TipoBajaId).HasColumnName("TipoBajaID");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Bajas)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bajas_Empleado");

            entity.HasOne(d => d.ProcesadoPor).WithMany(p => p.Bajas)
                .HasForeignKey(d => d.ProcesadoPorId)
                .HasConstraintName("FK_Bajas_ProcesadoPor");

            entity.HasOne(d => d.TipoBaja).WithMany(p => p.Bajas)
                .HasForeignKey(d => d.TipoBajaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bajas_TipoBaja");
        });

        modelBuilder.Entity<BeneficiosEmpleado>(entity =>
        {
            entity.HasKey(e => e.BeneficioEmpleadoId);

            entity.ToTable("BeneficiosEmpleado");

            entity.Property(e => e.BeneficioEmpleadoId).HasColumnName("BeneficioEmpleadoID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_BenefEmp_Activo");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.MontoMensual).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TipoBeneficioId).HasColumnName("TipoBeneficioID");

            entity.HasOne(d => d.Empleado).WithMany(p => p.BeneficiosEmpleados)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BenefEmp_Empleado");

            entity.HasOne(d => d.TipoBeneficio).WithMany(p => p.BeneficiosEmpleados)
                .HasForeignKey(d => d.TipoBeneficioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BenefEmp_TipoBeneficio");
        });

        modelBuilder.Entity<BitacoraAccione>(entity =>
        {
            entity.HasKey(e => e.BitacoraId);

            entity.Property(e => e.BitacoraId).HasColumnName("BitacoraID");
            entity.Property(e => e.Accion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("DireccionIP");
            entity.Property(e => e.FechaAccion)
                .HasDefaultValueSql("(getdate())", "DF_Bitacora_FechaAccion")
                .HasColumnType("datetime");
            entity.Property(e => e.RegistroId).HasColumnName("RegistroID");
            entity.Property(e => e.SesionId).HasColumnName("SesionID");
            entity.Property(e => e.Tabla)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Sesion).WithMany(p => p.BitacoraAcciones)
                .HasForeignKey(d => d.SesionId)
                .HasConstraintName("FK_Bitacora_Sesiones");

            entity.HasOne(d => d.Usuario).WithMany(p => p.BitacoraAcciones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bitacora_Usuarios");
        });

        modelBuilder.Entity<Capacitacione>(entity =>
        {
            entity.HasKey(e => e.CapacitacionId);

            entity.Property(e => e.CapacitacionId).HasColumnName("CapacitacionID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Institucion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Empleado).WithMany(p => p.Capacitaciones)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Capacitaciones_Empleado");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasIndex(e => e.NombreCargo, "UQ_Cargos_Nombre").IsUnique();

            entity.Property(e => e.CargoId).HasColumnName("CargoID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Cargos_Activo");
            entity.Property(e => e.CargoPadreId).HasColumnName("CargoPadreID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreCargo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SalarioBase).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CargoPadre).WithMany(p => p.InverseCargoPadre)
                .HasForeignKey(d => d.CargoPadreId)
                .HasConstraintName("FK_Cargos_Padre");
        });

        modelBuilder.Entity<CategoriaIngrediente>(entity =>
        {
            entity.ToTable("CategoriaIngrediente");

            entity.Property(e => e.CategoriaIngredienteId).HasColumnName("CategoriaIngredienteID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.ToTable("CategoriaProducto");

            entity.Property(e => e.CategoriaProductoId).HasColumnName("CategoriaProductoID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_CatProd_Activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.CiudadId);

            entity.HasIndex(e => e.DepartamentoGeoId, "IX_Ciudades_DepartamentoGeoID");

            entity.HasIndex(e => e.NombreCiudad, "UQ_Ciudades_Nombre").IsUnique();

            entity.Property(e => e.CiudadId).HasColumnName("CiudadID");
            entity.Property(e => e.DepartamentoGeoId).HasColumnName("DepartamentoGeoID");
            entity.Property(e => e.NombreCiudad)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DepartamentoGeo).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.DepartamentoGeoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ciudades_Departamentos");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasIndex(e => e.PersonaId, "UQ_Clientes_PersonaID").IsUnique();

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Cliente_Activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())", "DF_Cliente_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.LimiteCredito).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.TipoClienteId).HasColumnName("TipoClienteID");

            entity.HasOne(d => d.Persona).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Persona");

            entity.HasOne(d => d.TipoCliente).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.TipoClienteId)
                .HasConstraintName("FK_Clientes_TipoCliente");
        });

        modelBuilder.Entity<CodigosOtp>(entity =>
        {
            entity.HasKey(e => e.IdCodigo);

            entity.ToTable("CodigosOTP");

            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())", "DF_CodigosOTP_Fecha");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.CodigosOtps)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CodigosOTP_Usuarios");
        });

        modelBuilder.Entity<ConfiguracionSistema>(entity =>
        {
            entity.HasKey(e => e.ConfigId);

            entity.ToTable("ConfiguracionSistema");

            entity.HasIndex(e => e.Clave, "UQ_ConfiguracionSistema_Clave").IsUnique();

            entity.Property(e => e.ConfigId).HasColumnName("ConfigID");
            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaModificacion)
                .HasDefaultValueSql("(getdate())", "DF_Config_FechaMod")
                .HasColumnType("datetime");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("texto", "DF_Config_Tipo");
            entity.Property(e => e.Valor)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.UsuarioModificoNavigation).WithMany(p => p.ConfiguracionSistemas)
                .HasForeignKey(d => d.UsuarioModifico)
                .HasConstraintName("FK_Config_Usuarios");
        });

        modelBuilder.Entity<Contenido>(entity =>
        {
            entity.ToTable("Contenido");

            entity.HasIndex(e => e.AlmacenId, "IX_Contenido_AlmacenID");

            entity.HasIndex(e => e.ProductoId, "IX_Contenido_ProductoID");

            entity.HasIndex(e => new { e.AlmacenId, e.InventarioId }, "UQ_Contenido").IsUnique();

            entity.Property(e => e.ContenidoId).HasColumnName("ContenidoID");
            entity.Property(e => e.AlmacenId).HasColumnName("AlmacenID");
            entity.Property(e => e.CantidadDisponible).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.CantidadMinima).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.EstadoContenidoId).HasColumnName("EstadoContenidoID");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())", "DF_Cont_FechaAct")
                .HasColumnType("datetime");
            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

            entity.HasOne(d => d.Almacen).WithMany(p => p.Contenidos)
                .HasForeignKey(d => d.AlmacenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contenido_Almacen");

            entity.HasOne(d => d.EstadoContenido).WithMany(p => p.Contenidos)
                .HasForeignKey(d => d.EstadoContenidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contenido_EstadoCont");

            entity.HasOne(d => d.Inventario).WithMany(p => p.Contenidos)
                .HasForeignKey(d => d.InventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contenido_Inventario");

            entity.HasOne(d => d.Producto).WithMany(p => p.Contenidos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contenido_Producto");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasIndex(e => e.NumeroContrato, "UQ_Contratos_Numero").IsUnique();

            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Contrato_Activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.NumeroContrato)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contratos_Proveedor");
        });

        modelBuilder.Entity<ContratosEmpleado>(entity =>
        {
            entity.HasKey(e => e.ContratoEmpleadoId);

            entity.Property(e => e.ContratoEmpleadoId).HasColumnName("ContratoEmpleadoID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.EstadoContratoId).HasColumnName("EstadoContratoID");
            entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipoContratoId).HasColumnName("TipoContratoID");

            entity.HasOne(d => d.Empleado).WithMany(p => p.ContratosEmpleados)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContEmp_Empleado");

            entity.HasOne(d => d.EstadoContrato).WithMany(p => p.ContratosEmpleadoEstadoContratos)
                .HasForeignKey(d => d.EstadoContratoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContEmp_EstadoContrato");

            entity.HasOne(d => d.TipoContrato).WithMany(p => p.ContratosEmpleadoTipoContratos)
                .HasForeignKey(d => d.TipoContratoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContEmp_TipoContrato");
        });

        modelBuilder.Entity<ControlCalidadProduccion>(entity =>
        {
            entity.HasKey(e => e.ControlId).HasName("PK_ControlCalidad");

            entity.ToTable("ControlCalidadProduccion");

            entity.Property(e => e.ControlId).HasColumnName("ControlID");
            entity.Property(e => e.FechaControl)
                .HasDefaultValueSql("(getdate())", "DF_Control_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.InspectorId).HasColumnName("InspectorID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OrdenProduccionId).HasColumnName("OrdenProduccionID");

            entity.HasOne(d => d.Inspector).WithMany(p => p.ControlCalidadProduccions)
                .HasForeignKey(d => d.InspectorId)
                .HasConstraintName("FK_Control_Inspector");

            entity.HasOne(d => d.OrdenProduccion).WithMany(p => p.ControlCalidadProduccions)
                .HasForeignKey(d => d.OrdenProduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Control_OP");
        });

        modelBuilder.Entity<CuentasPagar>(entity =>
        {
            entity.HasKey(e => e.CuentaPagarId);

            entity.ToTable("CuentasPagar");

            entity.Property(e => e.CuentaPagarId).HasColumnName("CuentaPagarID");
            entity.Property(e => e.Monto).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.OrdenCompraId).HasColumnName("OrdenCompraID");
            entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");

            entity.HasOne(d => d.OrdenCompra).WithMany(p => p.CuentasPagars)
                .HasForeignKey(d => d.OrdenCompraId)
                .HasConstraintName("FK_CP_OrdenCompra");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.CuentasPagars)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CP_Proveedor");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.DepartamentoGeoId);

            entity.HasIndex(e => e.NombreDpto, "UQ_Departamentos_Nombre").IsUnique();

            entity.Property(e => e.DepartamentoGeoId).HasColumnName("DepartamentoGeoID");
            entity.Property(e => e.NombreDpto)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DepartamentosEmpresa>(entity =>
        {
            entity.HasKey(e => e.DepartamentoId);

            entity.ToTable("DepartamentosEmpresa");

            entity.HasIndex(e => e.NombreDepartamento, "UQ_DepartamentosEmpresa_Nombre").IsUnique();

            entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Depto_Activo");
            entity.Property(e => e.DepartamentoPadreId).HasColumnName("DepartamentoPadreID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.JefeEmpleadoId).HasColumnName("JefeEmpleadoID");
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.DepartamentoPadre).WithMany(p => p.InverseDepartamentoPadre)
                .HasForeignKey(d => d.DepartamentoPadreId)
                .HasConstraintName("FK_Depto_Padre");

            entity.HasOne(d => d.JefeEmpleado).WithMany(p => p.DepartamentosEmpresas)
                .HasForeignKey(d => d.JefeEmpleadoId)
                .HasConstraintName("FK_Depto_JefeEmpleado");
        });

        modelBuilder.Entity<Despacho>(entity =>
        {
            entity.Property(e => e.DespachoId).HasColumnName("DespachoID");
            entity.Property(e => e.MedioTransporteId).HasColumnName("MedioTransporteID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ResponsableId).HasColumnName("ResponsableID");

            entity.HasOne(d => d.MedioTransporte).WithMany(p => p.Despachos)
                .HasForeignKey(d => d.MedioTransporteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Despacho_Transporte");

            entity.HasOne(d => d.Responsable).WithMany(p => p.Despachos)
                .HasForeignKey(d => d.ResponsableId)
                .HasConstraintName("FK_Despacho_Responsable");
        });

        modelBuilder.Entity<DespachoDetalle>(entity =>
        {
            entity.ToTable("DespachoDetalle");

            entity.Property(e => e.DespachoDetalleId).HasColumnName("DespachoDetalleID");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.ContenidoId).HasColumnName("ContenidoID");
            entity.Property(e => e.DespachoId).HasColumnName("DespachoID");
            entity.Property(e => e.Destino)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Contenido).WithMany(p => p.DespachoDetalles)
                .HasForeignKey(d => d.ContenidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DD_Contenido");

            entity.HasOne(d => d.Despacho).WithMany(p => p.DespachoDetalles)
                .HasForeignKey(d => d.DespachoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DD_Despacho");
        });

        modelBuilder.Entity<DiscapacidadesPersona>(entity =>
        {
            entity.HasKey(e => e.DiscapacidadPersonaId);

            entity.ToTable("DiscapacidadesPersona");

            entity.HasIndex(e => new { e.PersonaId, e.TipoDiscapacidadId }, "UQ_DiscPer").IsUnique();

            entity.Property(e => e.DiscapacidadPersonaId).HasColumnName("DiscapacidadPersonaID");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("(getdate())", "DF_DiscPer_Fecha");
            entity.Property(e => e.GradoPorcentaje).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.NumeroCarnet)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("NumeroCARNET");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.TipoDiscapacidadId).HasColumnName("TipoDiscapacidadID");

            entity.HasOne(d => d.Persona).WithMany(p => p.DiscapacidadesPersonas)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiscPer_Persona");

            entity.HasOne(d => d.TipoDiscapacidad).WithMany(p => p.DiscapacidadesPersonas)
                .HasForeignKey(d => d.TipoDiscapacidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DiscPer_TipoDisc");
        });

        modelBuilder.Entity<DocumentosPersona>(entity =>
        {
            entity.HasKey(e => e.DocPersonaId);

            entity.ToTable("DocumentosPersona");

            entity.HasIndex(e => new { e.TipoDocumentoId, e.NumeroDocumento }, "UQ_DocPer_TipoNum").IsUnique();

            entity.Property(e => e.DocPersonaId).HasColumnName("DocPersonaID");
            entity.Property(e => e.Extension)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoID");

            entity.HasOne(d => d.Persona).WithMany(p => p.DocumentosPersonas)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocPer_Persona");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.DocumentosPersonas)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocPer_TipoDoc");
        });

        modelBuilder.Entity<DominioTipo>(entity =>
        {
            entity.ToTable("DominioTipo");

            entity.HasIndex(e => e.Descripcion, "UQ_DominioTipo_Descripcion").IsUnique();

            entity.Property(e => e.DominioTipoId).HasColumnName("DominioTipoID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DominioValor>(entity =>
        {
            entity.ToTable("DominioValor");

            entity.HasIndex(e => new { e.DominioTipoId, e.Descripcion }, "UQ_DominioValor").IsUnique();

            entity.Property(e => e.DominioValorId).HasColumnName("DominioValorID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_DomVal_Activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DominioTipoId).HasColumnName("DominioTipoID");

            entity.HasOne(d => d.DominioTipo).WithMany(p => p.DominioValors)
                .HasForeignKey(d => d.DominioTipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DominioValor_DominioTipo");
        });

        modelBuilder.Entity<EmailsPersona>(entity =>
        {
            entity.HasKey(e => e.EmailPersonaId);

            entity.ToTable("EmailsPersona");

            entity.HasIndex(e => e.Correo, "UQ_EmailsPersona_Correo").IsUnique();

            entity.Property(e => e.EmailPersonaId).HasColumnName("EmailPersonaID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_EmlPer_Activo");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.TipoContactoId).HasColumnName("TipoContactoID");

            entity.HasOne(d => d.Persona).WithMany(p => p.EmailsPersonas)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmlPer_Persona");

            entity.HasOne(d => d.TipoContacto).WithMany(p => p.EmailsPersonas)
                .HasForeignKey(d => d.TipoContactoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmlPer_TipoContacto");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasIndex(e => e.DepartamentoId, "IX_Empleados_DepartamentoID");

            entity.HasIndex(e => e.PersonaId, "IX_Empleados_PersonaID");

            entity.HasIndex(e => e.SucursalId, "IX_Empleados_SucursalID");

            entity.HasIndex(e => e.PersonaId, "UQ_Empleados_PersonaID").IsUnique();

            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.CargoId).HasColumnName("CargoID");
            entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");
            entity.Property(e => e.EstadoEmpleadoId).HasColumnName("EstadoEmpleadoID");
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");
            entity.Property(e => e.SupervisorId).HasColumnName("SupervisorID");

            entity.HasOne(d => d.Cargo).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_Cargo");

            entity.HasOne(d => d.Departamento).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_Depto");

            entity.HasOne(d => d.EstadoEmpleado).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.EstadoEmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_Estado");

            entity.HasOne(d => d.Persona).WithOne(p => p.Empleado)
                .HasForeignKey<Empleado>(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_Persona");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleados_Sucursal");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.InverseSupervisor)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK_Empleados_Supervisor");
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.EspecialidadId);

            entity.HasIndex(e => new { e.ProfesionId, e.Descripcion }, "UQ_Especialidades").IsUnique();

            entity.Property(e => e.EspecialidadId).HasColumnName("EspecialidadID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProfesionId).HasColumnName("ProfesionID");

            entity.HasOne(d => d.Profesion).WithMany(p => p.Especialidades)
                .HasForeignKey(d => d.ProfesionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Especialidades_Prof");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasIndex(e => e.NumeroFactura, "UQ_Facturas_Numero").IsUnique();

            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Descuento).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("(getdate())", "DF_Factura_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Impuesto).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PedidoId).HasColumnName("PedidoID");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Total)
                .HasComputedColumnSql("(([Subtotal]-[Descuento])+[Impuesto])", true)
                .HasColumnType("decimal(16, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Cliente");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Pedido");
        });

        modelBuilder.Entity<FacturaDetalle>(entity =>
        {
            entity.ToTable("FacturaDetalle");

            entity.Property(e => e.FacturaDetalleId).HasColumnName("FacturaDetalleID");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");
            entity.Property(e => e.PedidoDetalleId).HasColumnName("PedidoDetalleID");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", true)
                .HasColumnType("decimal(27, 8)");

            entity.HasOne(d => d.Factura).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacDet_Factura");

            entity.HasOne(d => d.PedidoDetalle).WithMany(p => p.FacturaDetalles)
                .HasForeignKey(d => d.PedidoDetalleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacDet_PedidoDetalle");
        });

        modelBuilder.Entity<HistorialCargo>(entity =>
        {
            entity.Property(e => e.HistorialCargoId).HasColumnName("HistorialCargoID");
            entity.Property(e => e.CargoId).HasColumnName("CargoID");
            entity.Property(e => e.DepartamentoId).HasColumnName("DepartamentoID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SalarioEnVigor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");

            entity.HasOne(d => d.Cargo).WithMany(p => p.HistorialCargos)
                .HasForeignKey(d => d.CargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HC_Cargo");

            entity.HasOne(d => d.Departamento).WithMany(p => p.HistorialCargos)
                .HasForeignKey(d => d.DepartamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HC_Departamento");

            entity.HasOne(d => d.Empleado).WithMany(p => p.HistorialCargos)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HC_Empleado");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.HistorialCargos)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HC_Sucursal");
        });

        modelBuilder.Entity<HorasExtra>(entity =>
        {
            entity.ToTable("HorasExtra");

            entity.Property(e => e.HorasExtraId).HasColumnName("HorasExtraID");
            entity.Property(e => e.AutorizadoPorId).HasColumnName("AutorizadoPorID");
            entity.Property(e => e.CantidadHoras).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.FechaAutorizacion).HasColumnType("datetime");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.AutorizadoPor).WithMany(p => p.HorasExtras)
                .HasForeignKey(d => d.AutorizadoPorId)
                .HasConstraintName("FK_HE_AutorizadoPor");

            entity.HasOne(d => d.Empleado).WithMany(p => p.HorasExtras)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HE_Empleado");
        });

        modelBuilder.Entity<IdiomasPersona>(entity =>
        {
            entity.HasKey(e => e.IdiomaPersonaId);

            entity.ToTable("IdiomasPersona");

            entity.HasIndex(e => new { e.PersonaId, e.IdiomaId }, "UQ_IdiomasPersona").IsUnique();

            entity.Property(e => e.IdiomaPersonaId).HasColumnName("IdiomaPersonaID");
            entity.Property(e => e.IdiomaId).HasColumnName("IdiomaID");
            entity.Property(e => e.NivelDominioId).HasColumnName("NivelDominioID");
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");

            entity.HasOne(d => d.Idioma).WithMany(p => p.IdiomasPersonaIdiomas)
                .HasForeignKey(d => d.IdiomaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdmPer_Idioma");

            entity.HasOne(d => d.NivelDominio).WithMany(p => p.IdiomasPersonaNivelDominios)
                .HasForeignKey(d => d.NivelDominioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdmPer_NivelDominio");

            entity.HasOne(d => d.Persona).WithMany(p => p.IdiomasPersonas)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdmPer_Persona");
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.Property(e => e.IngredienteId).HasColumnName("IngredienteID");
            entity.Property(e => e.CategoriaIngredienteId).HasColumnName("CategoriaIngredienteID");
            entity.Property(e => e.NombreIngrediente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UnidadBaseId).HasColumnName("UnidadBaseID");

            entity.HasOne(d => d.CategoriaIngrediente).WithMany(p => p.Ingredientes)
                .HasForeignKey(d => d.CategoriaIngredienteId)
                .HasConstraintName("FK_Ingred_Cat");

            entity.HasOne(d => d.UnidadBase).WithMany(p => p.Ingredientes)
                .HasForeignKey(d => d.UnidadBaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ingred_UM");
        });

        modelBuilder.Entity<InstitucionesEducativa>(entity =>
        {
            entity.HasKey(e => e.InstitucionId);

            entity.Property(e => e.InstitucionId).HasColumnName("InstitucionID");
            entity.Property(e => e.NombreInstitucion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Sigla)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoInstitucion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.ToTable("Inventario");

            entity.HasIndex(e => e.Lote, "IX_Inventario_Lote");

            entity.HasIndex(e => e.ProductoId, "IX_Inventario_ProductoID");

            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())", "DF_Inv_FechaReg")
                .HasColumnType("datetime");
            entity.Property(e => e.Lote)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.ProveedorRef)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Producto).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_Producto");
        });

        modelBuilder.Entity<LineaProduccion>(entity =>
        {
            entity.HasKey(e => e.LineaId);

            entity.ToTable("LineaProduccion");

            entity.Property(e => e.LineaId).HasColumnName("LineaID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Linea_Activo");
            entity.Property(e => e.NombreLinea)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.LineaProduccions)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK_Linea_Sucursal");
        });

        modelBuilder.Entity<MedioTransporte>(entity =>
        {
            entity.ToTable("MedioTransporte");

            entity.HasIndex(e => e.Placa, "UQ_MedioTransporte_Placa").IsUnique();

            entity.Property(e => e.MedioTransporteId).HasColumnName("MedioTransporteID");
            entity.Property(e => e.Conductor)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EstadoTransporteId).HasColumnName("EstadoTransporteID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Placa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");
            entity.Property(e => e.TipoVehiculo)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoTransporte).WithMany(p => p.MedioTransportes)
                .HasForeignKey(d => d.EstadoTransporteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedTransp_Estado");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.MedioTransportes)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK_MedTransp_Sucursal");
        });

        modelBuilder.Entity<MenuSistema>(entity =>
        {
            entity.HasKey(e => e.MenuId);

            entity.ToTable("MenuSistema");

            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_MenuSistema_Activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MenuPadreId).HasColumnName("MenuPadreID");
            entity.Property(e => e.NombreMenu)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ruta)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.MenuPadre).WithMany(p => p.InverseMenuPadre)
                .HasForeignKey(d => d.MenuPadreId)
                .HasConstraintName("FK_MenuSistema_MenuPadre");
        });

        modelBuilder.Entity<Merma>(entity =>
        {
            entity.ToTable("Merma");

            entity.HasIndex(e => e.ContenidoId, "IX_Merma_ContenidoID");

            entity.HasIndex(e => e.FechaMerma, "IX_Merma_FechaMerma");

            entity.Property(e => e.MermaId).HasColumnName("MermaID");
            entity.Property(e => e.CantidadPerdida).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.Causa)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ContenidoId).HasColumnName("ContenidoID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())", "DF_Merma_FechaReg")
                .HasColumnType("datetime");
            entity.Property(e => e.TipoMermaId).HasColumnName("TipoMermaID");
            entity.Property(e => e.UsuarioRegistroId).HasColumnName("UsuarioRegistroID");

            entity.HasOne(d => d.Contenido).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.ContenidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merma_Contenido");

            entity.HasOne(d => d.TipoMerma).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.TipoMermaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Merma_TipoMerma");

            entity.HasOne(d => d.UsuarioRegistro).WithMany(p => p.Mermas)
                .HasForeignKey(d => d.UsuarioRegistroId)
                .HasConstraintName("FK_Merma_Usuario");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("Movimiento");

            entity.HasIndex(e => e.FechaMovimiento, "IX_Movimiento_Fecha");

            entity.HasIndex(e => e.TipoMovimientoId, "IX_Movimiento_TipoMovimiento");

            entity.Property(e => e.MovimientoId).HasColumnName("MovimientoID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())", "DF_Mov_FechaReg")
                .HasColumnType("datetime");
            entity.Property(e => e.MedioTransporteId).HasColumnName("MedioTransporteID");
            entity.Property(e => e.MovimientoDetalleId).HasColumnName("MovimientoDetalleID");
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoMovimientoId).HasColumnName("TipoMovimientoID");
            entity.Property(e => e.UsuarioRegistroId).HasColumnName("UsuarioRegistroID");

            entity.HasOne(d => d.MedioTransporte).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MedioTransporteId)
                .HasConstraintName("FK_Mov_MedioTransporte");

            entity.HasOne(d => d.MovimientoDetalle).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.MovimientoDetalleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mov_MovimientoDetalle");

            entity.HasOne(d => d.TipoMovimiento).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.TipoMovimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mov_TipoMovimiento");

            entity.HasOne(d => d.UsuarioRegistro).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.UsuarioRegistroId)
                .HasConstraintName("FK_Mov_Usuario");
        });

        modelBuilder.Entity<MovimientoDetalle>(entity =>
        {
            entity.ToTable("MovimientoDetalle");

            entity.HasIndex(e => e.ContenidoId, "IX_MovDet_ContenidoID");

            entity.Property(e => e.MovimientoDetalleId).HasColumnName("MovimientoDetalleID");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.ContenidoId).HasColumnName("ContenidoID");
            entity.Property(e => e.Destino)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Origen)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Contenido).WithMany(p => p.MovimientoDetalles)
                .HasForeignKey(d => d.ContenidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovDet_Contenido");
        });

        modelBuilder.Entity<OrdenProduccion>(entity =>
        {
            entity.ToTable("OrdenProduccion");

            entity.HasIndex(e => e.NumeroOrden, "UQ_OP_NumeroOrden").IsUnique();

            entity.Property(e => e.OrdenProduccionId).HasColumnName("OrdenProduccionID");
            entity.Property(e => e.CantidadPlanificada).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CantidadProducida).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EstadoProduccionId).HasColumnName("EstadoProduccionID");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.LineaId).HasColumnName("LineaID");
            entity.Property(e => e.NumeroOrden)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PresentacionId).HasColumnName("PresentacionID");
            entity.Property(e => e.PrioridadId).HasColumnName("PrioridadID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.RecetaId).HasColumnName("RecetaID");
            entity.Property(e => e.ResponsableId).HasColumnName("ResponsableID");
            entity.Property(e => e.TurnoId).HasColumnName("TurnoID");

            entity.HasOne(d => d.EstadoProduccion).WithMany(p => p.OrdenProduccionEstadoProduccions)
                .HasForeignKey(d => d.EstadoProduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OP_Estado");

            entity.HasOne(d => d.Linea).WithMany(p => p.OrdenProduccions)
                .HasForeignKey(d => d.LineaId)
                .HasConstraintName("FK_OP_Linea");

            entity.HasOne(d => d.Presentacion).WithMany(p => p.OrdenProduccions)
                .HasForeignKey(d => d.PresentacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OP_Presentacion");

            entity.HasOne(d => d.Prioridad).WithMany(p => p.OrdenProduccionPrioridads)
                .HasForeignKey(d => d.PrioridadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OP_Prioridad");

            entity.HasOne(d => d.Producto).WithMany(p => p.OrdenProduccions)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OP_Producto");

            entity.HasOne(d => d.Receta).WithMany(p => p.OrdenProduccions)
                .HasForeignKey(d => d.RecetaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OP_Receta");

            entity.HasOne(d => d.Responsable).WithMany(p => p.OrdenProduccions)
                .HasForeignKey(d => d.ResponsableId)
                .HasConstraintName("FK_OP_Responsable");

            entity.HasOne(d => d.Turno).WithMany(p => p.OrdenProduccionTurnos)
                .HasForeignKey(d => d.TurnoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OP_Turno");
        });

        modelBuilder.Entity<OrdenesCompra>(entity =>
        {
            entity.HasKey(e => e.OrdenCompraId);

            entity.ToTable("OrdenesCompra");

            entity.HasIndex(e => e.NumeroOrden, "UQ_OrdenesCompra_Numero").IsUnique();

            entity.Property(e => e.OrdenCompraId).HasColumnName("OrdenCompraID");
            entity.Property(e => e.ContratoId).HasColumnName("ContratoID");
            entity.Property(e => e.FechaEmision).HasDefaultValueSql("(getdate())", "DF_OC_Fecha");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.NumeroOrden)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");
            entity.Property(e => e.UsuarioCreadorId).HasColumnName("UsuarioCreadorID");

            entity.HasOne(d => d.Contrato).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.ContratoId)
                .HasConstraintName("FK_OC_Contrato");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OC_Proveedor");

            entity.HasOne(d => d.UsuarioCreador).WithMany(p => p.OrdenesCompras)
                .HasForeignKey(d => d.UsuarioCreadorId)
                .HasConstraintName("FK_OC_Usuario");
        });

        modelBuilder.Entity<PagosCliente>(entity =>
        {
            entity.HasKey(e => e.PagoClienteId);

            entity.Property(e => e.PagoClienteId).HasColumnName("PagoClienteID");
            entity.Property(e => e.FacturaId).HasColumnName("FacturaID");
            entity.Property(e => e.MedioPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Efectivo", "DF_Pago_Medio");
            entity.Property(e => e.Monto).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Referencia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Factura).WithMany(p => p.PagosClientes)
                .HasForeignKey(d => d.FacturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_Factura");

            entity.HasOne(d => d.Usuario).WithMany(p => p.PagosClientes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Pago_Usuario");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasIndex(e => e.NumeroPedido, "UQ_Pedidos_Numero").IsUnique();

            entity.Property(e => e.PedidoId).HasColumnName("PedidoID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.EstadoPedido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Pendiente", "DF_Pedido_Estado");
            entity.Property(e => e.FechaPedido)
                .HasDefaultValueSql("(getdate())", "DF_Pedido_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.NumeroPedido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");
            entity.Property(e => e.UsuarioVendedorId).HasColumnName("UsuarioVendedorID");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedidos_Cliente");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK_Pedidos_Sucursal");

            entity.HasOne(d => d.UsuarioVendedor).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioVendedorId)
                .HasConstraintName("FK_Pedidos_Vendedor");
        });

        modelBuilder.Entity<PedidoDetalle>(entity =>
        {
            entity.ToTable("PedidoDetalle");

            entity.Property(e => e.PedidoDetalleId).HasColumnName("PedidoDetalleID");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.Descuento).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.PedidoId).HasColumnName("PedidoID");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.PresentacionId).HasColumnName("PresentacionID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("(([Cantidad]*[PrecioUnitario])*((1)-[Descuento]/(100)))", true)
                .HasColumnType("decimal(38, 14)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedDet_Pedido");

            entity.HasOne(d => d.Presentacion).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.PresentacionId)
                .HasConstraintName("FK_PedDet_Presentacion");

            entity.HasOne(d => d.Producto).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedDet_Producto");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasIndex(e => new { e.Modulo, e.Accion, e.NombrePermiso }, "UQ_Permisos_ModuloAccion").IsUnique();

            entity.Property(e => e.PermisoId).HasColumnName("PermisoID");
            entity.Property(e => e.Accion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Permisos_Activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Modulo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.ToTable("Persona");

            entity.HasIndex(e => e.ApellidoPat, "IX_Persona_ApellidoPat");

            entity.HasIndex(e => e.NumeroDocumento, "IX_Persona_Documento");

            entity.HasIndex(e => e.Estado, "IX_Persona_Estado");

            entity.HasIndex(e => new { e.TipoDocumentoId, e.NumeroDocumento }, "UQ_Persona_Documento").IsUnique();

            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.ApellidoMat)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPat)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CiudadResidenciaId).HasColumnName("CiudadResidenciaID");
            entity.Property(e => e.CorreoPrincipal)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.DireccionLinea1)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DireccionLinea2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("Activo", "DF_Persona_Estado");
            entity.Property(e => e.EstadoCivilId).HasColumnName("EstadoCivilID");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())", "DF_Persona_FechaReg")
                .HasColumnType("datetime");
            entity.Property(e => e.FotoUrl)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("FotoURL");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocExtension)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.SexoId).HasColumnName("SexoID");
            entity.Property(e => e.TelefonoPrincipal)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumentoId).HasColumnName("TipoDocumentoID");
            entity.Property(e => e.TipoSangreId).HasColumnName("TipoSangreID");
            entity.Property(e => e.ZonaId).HasColumnName("ZonaID");

            entity.HasOne(d => d.CiudadResidencia).WithMany(p => p.Personas)
                .HasForeignKey(d => d.CiudadResidenciaId)
                .HasConstraintName("FK_Persona_Ciudad");

            entity.HasOne(d => d.EstadoCivil).WithMany(p => p.PersonaEstadoCivils)
                .HasForeignKey(d => d.EstadoCivilId)
                .HasConstraintName("FK_Persona_EstadoCivil");

            entity.HasOne(d => d.Sexo).WithMany(p => p.PersonaSexos)
                .HasForeignKey(d => d.SexoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona_Sexo");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.PersonaTipoDocumentos)
                .HasForeignKey(d => d.TipoDocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Persona_TipoDoc");

            entity.HasOne(d => d.TipoSangre).WithMany(p => p.PersonaTipoSangres)
                .HasForeignKey(d => d.TipoSangreId)
                .HasConstraintName("FK_Persona_TipoSangre");

            entity.HasOne(d => d.Zona).WithMany(p => p.Personas)
                .HasForeignKey(d => d.ZonaId)
                .HasConstraintName("FK_Persona_Zona");
        });

        modelBuilder.Entity<Planilla>(entity =>
        {
            entity.ToTable("Planilla");

            entity.HasIndex(e => new { e.EmpleadoId, e.Anio }, "IX_Planilla_EmpleadoAnio");

            entity.HasIndex(e => new { e.EmpleadoId, e.Mes, e.Anio }, "UQ_Planilla_EmpleadoMes").IsUnique();

            entity.Property(e => e.PlanillaId).HasColumnName("PlanillaID");
            entity.Property(e => e.Bonos).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descuentos).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.HaberBasico).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalLiquido)
                .HasComputedColumnSql("(([HaberBasico]+[Bonos])-[Descuentos])", true)
                .HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Planillas)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Planilla_Empleado");
        });

        modelBuilder.Entity<PlanillaDetalle>(entity =>
        {
            entity.HasKey(e => e.DetalleId);

            entity.ToTable("PlanillaDetalle");

            entity.HasIndex(e => e.PlanillaId, "UQ_PlanillaDetalle_Planilla").IsUnique();

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");
            entity.Property(e => e.Afp)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("AFP");
            entity.Property(e => e.Cns)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("CNS");
            entity.Property(e => e.PlanillaId).HasColumnName("PlanillaID");
            entity.Property(e => e.RcIva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("RC_IVA");

            entity.HasOne(d => d.Planilla).WithOne(p => p.PlanillaDetalle)
                .HasForeignKey<PlanillaDetalle>(d => d.PlanillaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PlanillaDetalle_Planilla");
        });

        modelBuilder.Entity<Presentacione>(entity =>
        {
            entity.HasKey(e => e.PresentacionId);

            entity.Property(e => e.PresentacionId).HasColumnName("PresentacionID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Present_Activo");
            entity.Property(e => e.Capacidad).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NombrePresentacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UnidadMedidaId).HasColumnName("UnidadMedidaID");

            entity.HasOne(d => d.UnidadMedida).WithMany(p => p.Presentaciones)
                .HasForeignKey(d => d.UnidadMedidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Presentaciones_UM");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("Producto");

            entity.HasIndex(e => e.NombreProducto, "IX_Producto_Nombre");

            entity.HasIndex(e => e.CodigoSku, "UQ_Producto_SKU").IsUnique();

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Producto_Activo");
            entity.Property(e => e.CategoriaProductoId).HasColumnName("CategoriaProductoID");
            entity.Property(e => e.CodigoSku)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("CodigoSKU");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_Producto_FechaCreacion")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UnidadMedidaTexto)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.CategoriaProducto).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaProductoId)
                .HasConstraintName("FK_Producto_Categoria");
        });

        modelBuilder.Entity<Profesione>(entity =>
        {
            entity.HasKey(e => e.ProfesionId);

            entity.HasIndex(e => e.NombreProfesion, "UQ_Profesiones_Nombre").IsUnique();

            entity.Property(e => e.ProfesionId).HasColumnName("ProfesionID");
            entity.Property(e => e.AreaConocimientoId).HasColumnName("AreaConocimientoID");
            entity.Property(e => e.NombreProfesion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.AreaConocimiento).WithMany(p => p.Profesiones)
                .HasForeignKey(d => d.AreaConocimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profesiones_Area");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedorId);

            entity.HasIndex(e => e.PersonaId, "UQ_Proveedores_PersonaID").IsUnique();

            entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Proveedor_Activo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())", "DF_Proveedor_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.Nit)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NIT");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Persona).WithOne(p => p.Proveedore)
                .HasForeignKey<Proveedore>(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proveedor_Persona");
        });

        modelBuilder.Entity<RecepcionMercaderium>(entity =>
        {
            entity.HasKey(e => e.RecepcionId);

            entity.Property(e => e.RecepcionId).HasColumnName("RecepcionID");
            entity.Property(e => e.AlmacenId).HasColumnName("AlmacenID");
            entity.Property(e => e.CantidadRecibida).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.InventarioId).HasColumnName("InventarioID");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.OrdenCompraId).HasColumnName("OrdenCompraID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Almacen).WithMany(p => p.RecepcionMercaderia)
                .HasForeignKey(d => d.AlmacenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recepcion_Almacen");

            entity.HasOne(d => d.Inventario).WithMany(p => p.RecepcionMercaderia)
                .HasForeignKey(d => d.InventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recepcion_Inventario");

            entity.HasOne(d => d.OrdenCompra).WithMany(p => p.RecepcionMercaderia)
                .HasForeignKey(d => d.OrdenCompraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recepcion_OrdenCompra");

            entity.HasOne(d => d.Usuario).WithMany(p => p.RecepcionMercaderia)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Recepcion_Usuario");
        });

        modelBuilder.Entity<Receta>(entity =>
        {
            entity.Property(e => e.RecetaId).HasColumnName("RecetaID");
            entity.Property(e => e.EsActiva).HasDefaultValue(true, "DF_Receta_Activa");
            entity.Property(e => e.EstadoRecetaId).HasColumnName("EstadoRecetaID");
            entity.Property(e => e.FechaAprobacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_Receta_FechaCreacion")
                .HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.TipoRecetaId).HasColumnName("TipoRecetaID");
            entity.Property(e => e.UsuarioAprobacionId).HasColumnName("UsuarioAprobacionID");
            entity.Property(e => e.UsuarioCreacionId).HasColumnName("UsuarioCreacionID");
            entity.Property(e => e.Version)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoReceta).WithMany(p => p.RecetaEstadoReceta)
                .HasForeignKey(d => d.EstadoRecetaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recetas_Estado");

            entity.HasOne(d => d.Producto).WithMany(p => p.Receta)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recetas_Producto");

            entity.HasOne(d => d.TipoReceta).WithMany(p => p.RecetaTipoReceta)
                .HasForeignKey(d => d.TipoRecetaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recetas_Tipo");

            entity.HasOne(d => d.UsuarioAprobacion).WithMany(p => p.RecetaUsuarioAprobacions)
                .HasForeignKey(d => d.UsuarioAprobacionId)
                .HasConstraintName("FK_Recetas_UserA");

            entity.HasOne(d => d.UsuarioCreacion).WithMany(p => p.RecetaUsuarioCreacions)
                .HasForeignKey(d => d.UsuarioCreacionId)
                .HasConstraintName("FK_Recetas_UserC");
        });

        modelBuilder.Entity<RecetaDetalle>(entity =>
        {
            entity.ToTable("RecetaDetalle");

            entity.Property(e => e.RecetaDetalleId).HasColumnName("RecetaDetalleID");
            entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IngredienteId).HasColumnName("IngredienteID");
            entity.Property(e => e.Merma).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.RecetaId).HasColumnName("RecetaID");
            entity.Property(e => e.UnidadMedidaId).HasColumnName("UnidadMedidaID");

            entity.HasOne(d => d.Ingrediente).WithMany(p => p.RecetaDetalles)
                .HasForeignKey(d => d.IngredienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecDet_Ingred");

            entity.HasOne(d => d.Receta).WithMany(p => p.RecetaDetalles)
                .HasForeignKey(d => d.RecetaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecDet_Receta");

            entity.HasOne(d => d.UnidadMedida).WithMany(p => p.RecetaDetalles)
                .HasForeignKey(d => d.UnidadMedidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecDet_UM");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId);

            entity.HasIndex(e => e.NombreRol, "UQ_Roles_NombreRol").IsUnique();

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Roles_Activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_Roles_FechaCreacion")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesMenu>(entity =>
        {
            entity.HasKey(e => e.RolMenuId);

            entity.ToTable("RolesMenu");

            entity.HasIndex(e => new { e.RolId, e.MenuId }, "UQ_RolesMenu").IsUnique();

            entity.Property(e => e.RolMenuId).HasColumnName("RolMenuID");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())", "DF_RolesMenu_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.RolId).HasColumnName("RolID");

            entity.HasOne(d => d.Menu).WithMany(p => p.RolesMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolesMenu_Menu");

            entity.HasOne(d => d.Rol).WithMany(p => p.RolesMenus)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolesMenu_Roles");
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
        {
            entity.HasKey(e => e.RolPermisoId);

            entity.HasIndex(e => new { e.RolId, e.PermisoId }, "UQ_RolesPermisos").IsUnique();

            entity.Property(e => e.RolPermisoId).HasColumnName("RolPermisoID");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())", "DF_RolesPermisos_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.PermisoId).HasColumnName("PermisoID");
            entity.Property(e => e.RolId).HasColumnName("RolID");

            entity.HasOne(d => d.Permiso).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.PermisoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolesPermisos_Permisos");

            entity.HasOne(d => d.Rol).WithMany(p => p.RolesPermisos)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RolesPermisos_Roles");
        });

        modelBuilder.Entity<SaldoVacacione>(entity =>
        {
            entity.HasKey(e => e.SaldoVacacionId);

            entity.HasIndex(e => new { e.EmpleadoId, e.Anio }, "UQ_SaldoVac_EmpleadoAnio").IsUnique();

            entity.Property(e => e.SaldoVacacionId).HasColumnName("SaldoVacacionID");
            entity.Property(e => e.DiasRestantes).HasComputedColumnSql("([DiasCorresponde]-[DiasUsados])", true);
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");

            entity.HasOne(d => d.Empleado).WithMany(p => p.SaldoVacaciones)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaldoVac_Empleado");
        });

        modelBuilder.Entity<Sancione>(entity =>
        {
            entity.HasKey(e => e.SancionId);

            entity.Property(e => e.SancionId).HasColumnName("SancionID");
            entity.Property(e => e.AplicadaPorId).HasColumnName("AplicadaPorID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.Motivo)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.TipoSancionId).HasColumnName("TipoSancionID");

            entity.HasOne(d => d.AplicadaPor).WithMany(p => p.Sanciones)
                .HasForeignKey(d => d.AplicadaPorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sanciones_AplicadaPor");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Sanciones)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sanciones_Empleado");

            entity.HasOne(d => d.TipoSancion).WithMany(p => p.Sanciones)
                .HasForeignKey(d => d.TipoSancionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sanciones_Tipo");
        });

        modelBuilder.Entity<Sesione>(entity =>
        {
            entity.HasKey(e => e.SesionId);

            entity.Property(e => e.SesionId).HasColumnName("SesionID");
            entity.Property(e => e.Activa).HasDefaultValue(true, "DF_Sesiones_Activa");
            entity.Property(e => e.DireccionIp)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("DireccionIP");
            entity.Property(e => e.Dispositivo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCierre).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())", "DF_Sesiones_FechaInicio")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Sesiones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sesiones_Usuarios");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.SucursalId);

            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Sucursales_Activo");
            entity.Property(e => e.CiudadId).HasColumnName("CiudadID");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_Sucursales_FechaCreacion")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.CiudadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sucursales_Ciudades");
        });

        modelBuilder.Entity<TelefonosPersona>(entity =>
        {
            entity.HasKey(e => e.TelefonoPersonaId);

            entity.ToTable("TelefonosPersona");

            entity.Property(e => e.TelefonoPersonaId).HasColumnName("TelefonoPersonaID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_TelPer_Activo");
            entity.Property(e => e.CodigoPais)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("+591", "DF_TelPer_Codigo");
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.TipoContactoId).HasColumnName("TipoContactoID");

            entity.HasOne(d => d.Persona).WithMany(p => p.TelefonosPersonas)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TelPer_Persona");

            entity.HasOne(d => d.TipoContacto).WithMany(p => p.TelefonosPersonas)
                .HasForeignKey(d => d.TipoContactoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TelPer_TipoContacto");
        });

        modelBuilder.Entity<TiposCliente>(entity =>
        {
            entity.HasKey(e => e.TipoClienteId);

            entity.ToTable("TiposCliente");

            entity.Property(e => e.TipoClienteId).HasColumnName("TipoClienteID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_TipoCli_Activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TitulosPersona>(entity =>
        {
            entity.HasKey(e => e.TituloPersonaId);

            entity.ToTable("TitulosPersona");

            entity.Property(e => e.TituloPersonaId).HasColumnName("TituloPersonaID");
            entity.Property(e => e.EspecialidadId).HasColumnName("EspecialidadID");
            entity.Property(e => e.InstitucionId).HasColumnName("InstitucionID");
            entity.Property(e => e.NivelEducativoId).HasColumnName("NivelEducativoID");
            entity.Property(e => e.NumeroDiploma)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.ProfesionId).HasColumnName("ProfesionID");
            entity.Property(e => e.TituloObtenido)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Especialidad).WithMany(p => p.TitulosPersonas)
                .HasForeignKey(d => d.EspecialidadId)
                .HasConstraintName("FK_TitPer_Especialidad");

            entity.HasOne(d => d.Institucion).WithMany(p => p.TitulosPersonas)
                .HasForeignKey(d => d.InstitucionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitPer_Institucion");

            entity.HasOne(d => d.NivelEducativo).WithMany(p => p.TitulosPersonas)
                .HasForeignKey(d => d.NivelEducativoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitPer_NivEduc");

            entity.HasOne(d => d.Persona).WithMany(p => p.TitulosPersonas)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitPer_Persona");

            entity.HasOne(d => d.Profesion).WithMany(p => p.TitulosPersonas)
                .HasForeignKey(d => d.ProfesionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TitPer_Profesion");
        });

        modelBuilder.Entity<UnidadMedidum>(entity =>
        {
            entity.HasKey(e => e.UnidadMedidaId);

            entity.HasIndex(e => e.Nombre, "UQ_UnidadMedida_Nombre").IsUnique();

            entity.Property(e => e.UnidadMedidaId).HasColumnName("UnidadMedidaID");
            entity.Property(e => e.Abreviatura)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoUnidad)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasIndex(e => e.NombreUsuario, "UQ_Usuarios_NombreUsuario").IsUnique();

            entity.HasIndex(e => e.PersonaId, "UQ_Usuarios_PersonaID").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_Usuarios_Activo");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_Usuarios_FechaCreacion")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PersonaId).HasColumnName("PersonaID");
            entity.Property(e => e.UltimoAcceso).HasColumnType("datetime");

            entity.HasOne(d => d.Persona).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.PersonaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Persona");
        });

        modelBuilder.Entity<UsuariosRole>(entity =>
        {
            entity.HasKey(e => e.UsuarioRolId);

            entity.HasIndex(e => new { e.UsuarioId, e.RolId }, "UQ_UsuariosRoles").IsUnique();

            entity.Property(e => e.UsuarioRolId).HasColumnName("UsuarioRolID");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())", "DF_UsuariosRoles_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuariosRoles_Roles");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuariosRoles_Usuarios");
        });

        modelBuilder.Entity<UsuariosSucursale>(entity =>
        {
            entity.HasKey(e => e.UsuarioSucursalId);

            entity.HasIndex(e => new { e.UsuarioId, e.SucursalId }, "UQ_UsuariosSucursales").IsUnique();

            entity.Property(e => e.UsuarioSucursalId).HasColumnName("UsuarioSucursalID");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("(getdate())", "DF_UsuariosSuc_Fecha")
                .HasColumnType("datetime");
            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.UsuariosSucursales)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuariosSucursales_Sucursales");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuariosSucursales)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuariosSucursales_Usuarios");
        });

        modelBuilder.Entity<Vacacione>(entity =>
        {
            entity.HasKey(e => e.VacacionId);

            entity.Property(e => e.VacacionId).HasColumnName("VacacionID");
            entity.Property(e => e.AprobadoPorId).HasColumnName("AprobadoPorID");
            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.EstadoVacacionId).HasColumnName("EstadoVacacionID");
            entity.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("(getdate())", "DF_Vacaciones_FechaSolicitud")
                .HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.AprobadoPor).WithMany(p => p.Vacaciones)
                .HasForeignKey(d => d.AprobadoPorId)
                .HasConstraintName("FK_Vacaciones_Aprobador");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Vacaciones)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vacaciones_Empleado");

            entity.HasOne(d => d.EstadoVacacion).WithMany(p => p.Vacaciones)
                .HasForeignKey(d => d.EstadoVacacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vacaciones_Estado");
        });

        modelBuilder.Entity<VwEmpleadoCompleto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_EmpleadoCompleto");

            entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");
            entity.Property(e => e.EstadoEmpleado)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreCargo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(202)
                .IsUnicode(false);
            entity.Property(e => e.NombreDepartamento)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreSupervisor)
                .HasMaxLength(151)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salario).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<VwMerma>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Mermas");

            entity.Property(e => e.CantidadPerdida).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.Causa)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CodigoSku)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("CodigoSKU");
            entity.Property(e => e.Lote)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.MermaId).HasColumnName("MermaID");
            entity.Property(e => e.NombreAlmacen)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoMerma)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwOrdenesProduccion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_OrdenesProduccion");

            entity.Property(e => e.CantidadPlanificada).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CantidadProducida).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EstadoProduccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.NombreLinea)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombrePresentacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NumeroOrden)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrdenProduccionId).HasColumnName("OrdenProduccionID");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Responsable)
                .HasMaxLength(151)
                .IsUnicode(false);
            entity.Property(e => e.Turno)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.VersionReceta)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwPedidosCompleto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_PedidosCompleto");

            entity.Property(e => e.CorreoPrincipal)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.EstadoPedido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FechaPedido).HasColumnType("datetime");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(151)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreVendedor)
                .HasMaxLength(151)
                .IsUnicode(false);
            entity.Property(e => e.NumeroPedido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PedidoId).HasColumnName("PedidoID");
            entity.Property(e => e.TelefonoPrincipal)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoCliente)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwStockAlmacen>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_StockAlmacen");

            entity.Property(e => e.AlmacenId).HasColumnName("AlmacenID");
            entity.Property(e => e.CantidadDisponible).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.CantidadMinima).HasColumnType("decimal(14, 4)");
            entity.Property(e => e.CodigoSku)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("CodigoSKU");
            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(12, 4)");
            entity.Property(e => e.EstadoAlmacen)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoContenido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.Lote)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.NombreAlmacen)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.TipoAlmacen)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.Property(e => e.ZonaId).HasColumnName("ZonaID");
            entity.Property(e => e.CiudadId).HasColumnName("CiudadID");
            entity.Property(e => e.NombreZona)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Zonas)
                .HasForeignKey(d => d.CiudadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zonas_Ciudades");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
