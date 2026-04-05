using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ── Módulo Base ────────────────────────────────────────────
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CodigoOTP> CodigosOTP { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }

        // ── Módulo 6: RRHH ────────────────────────────────────────
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<DepartamentoEmpresa> DepartamentosEmpresa { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Planilla> Planillas { get; set; }
        public DbSet<PlanillaDetalle> PlanillaDetalles { get; set; }
        public DbSet<Vacacion> Vacaciones { get; set; }

        // ── Tablas de seguridad referenciadas por RRHH ────────────
        public DbSet<Sucursal> Sucursales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Persona ───────────────────────────────────────────
            modelBuilder.Entity<Persona>()
                .HasIndex(p => p.CI).IsUnique();

            // ── Usuario ───────────────────────────────────────────
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario).IsUnique();
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.PersonaID).IsUnique();

            // ── Empleado: autorreferencia supervisor ──────────────
            modelBuilder.Entity<Empleado>()
                .HasOne(e => e.Supervisor)
                .WithMany()
                .HasForeignKey(e => e.SupervisorID)
                .OnDelete(DeleteBehavior.NoAction);

            // ── DepartamentoEmpresa: autorreferencia padre ────────
            modelBuilder.Entity<DepartamentoEmpresa>()
                .HasOne(d => d.DepartamentoPadre)
                .WithMany()
                .HasForeignKey(d => d.DepartamentoPadreID)
                .OnDelete(DeleteBehavior.NoAction);

            // ── DepartamentoEmpresa: jefe (evitar ciclo) ──────────
            modelBuilder.Entity<DepartamentoEmpresa>()
                .HasOne(d => d.JefeEmpleado)
                .WithMany()
                .HasForeignKey(d => d.JefeEmpleadoID)
                .OnDelete(DeleteBehavior.NoAction);

            // ── Cargo: autorreferencia padre ──────────────────────
            modelBuilder.Entity<Cargo>()
                .HasOne(c => c.CargoPadre)
                .WithMany()
                .HasForeignKey(c => c.CargoPadreID)
                .OnDelete(DeleteBehavior.NoAction);

            // ── Planilla: columna calculada ───────────────────────
            modelBuilder.Entity<Planilla>()
                .Property(p => p.TotalLiquido)
                .ValueGeneratedOnAddOrUpdate();

            // ── PlanillaDetalle: relación 1:1 ─────────────────────
            modelBuilder.Entity<PlanillaDetalle>()
                .HasOne(pd => pd.Planilla)
                .WithOne(p => p.Detalle)
                .HasForeignKey<PlanillaDetalle>(pd => pd.PlanillaID);

            // ── Asistencia: unicidad empleado+fecha ───────────────
            modelBuilder.Entity<Asistencia>()
                .HasIndex(a => new { a.EmpleadoID, a.Fecha }).IsUnique();

            // ── Planilla: unicidad empleado+mes+año ───────────────
            modelBuilder.Entity<Planilla>()
                .HasIndex(p => new { p.EmpleadoID, p.Mes, p.Anio }).IsUnique();

            // ── Empleado: unicidad PersonaID ──────────────────────
            modelBuilder.Entity<Empleado>()
                .HasIndex(e => e.PersonaID).IsUnique();
        }
    }
}
