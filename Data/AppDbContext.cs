using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CodigoOTP> CodigosOTP { get; set; }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aseguramos que EF Core entienda que el CI es único, tal como en tu SQL
            modelBuilder.Entity<Persona>()
                .HasIndex(p => p.CI)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();
                
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.PersonaID)
                .IsUnique();
        }
    }
}