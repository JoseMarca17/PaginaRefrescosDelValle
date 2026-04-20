using Microsoft.EntityFrameworkCore;
using PaginaRefrescosDelValle.Models.Entities;

namespace PaginaRefrescosDelValle.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // --- MÓDULO DE CLIENTES Y VENTAS ---
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TipoCliente> TipoClientes { get; set; }

        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVentas { get; set; } 

        // --- MÓDULO DE LOGÍSTICA Y UBICACIÓN ---
        // (Asegúrate de tener estas entidades en Models/Entities)
        // public DbSet<Pais> Paises { get; set; }
        // public DbSet<Departamento> Departamentos { get; set; }
        // public DbSet<Ciudad> Ciudades { get; set; }

        // --- MÓDULO DE INVENTARIO ---
        // public DbSet<Producto> Productos { get; set; }
        // public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aquí puedes agregar configuraciones extra si José te lo pide luego
        }
    }
}