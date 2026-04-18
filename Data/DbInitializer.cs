using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace RefrescosDelValle.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            Console.WriteLine("\n[SYS] --- INICIANDO SINCRONIZACIÓN DE ADMINS ---");

            // 1. Asegurar que el Rol 'SuperAdmin' exista (Indispensable)
            var rolAdmin = await context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "SuperAdmin");
            if (rolAdmin == null)
            {
                rolAdmin = new Role { NombreRol = "SuperAdmin", Activo = true };
                context.Roles.Add(rolAdmin);
                await context.SaveChangesAsync();
                Console.WriteLine("[SYS] Rol 'SuperAdmin' creado.");
            }

            // 2. Obtener lista de Admins desde el JSON
            // Esto mapea tanto el objeto único como una lista si decides cambiar el formato
            var adminsToCreate = config.GetSection("AdminSetup").GetChildren().Any() 
                ? config.GetSection("AdminSetup").Get<List<AdminConfig>>() // Si lo vuelves lista
                : new List<AdminConfig> { new AdminConfig { // O mantener el actual por defecto
                    Email = config["AdminSetup:Email"], 
                    Username = config["AdminSetup:Username"], 
                    Password = config["AdminSetup:Password"] 
                }};

            // 3. IDs Auxiliares
            var tipoDocCI = await context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 10 && d.Descripcion == "Carnet de Identidad");
            var sexoIndet = await context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 1 && d.Descripcion == "Indeterminado");

            foreach (var admin in adminsToCreate.Where(a => !string.IsNullOrEmpty(a.Email)))
            {
                // CRÍTICO: Verificar si el usuario ya existe por Email para no duplicar
                var existePersona = await context.Personas.AnyAsync(p => p.CorreoPrincipal == admin.Email);
                
                if (!existePersona)
                {
                    Console.WriteLine($"[SYS] Inyectando nuevo admin: {admin.Email}...");

                    var persona = new Persona
                    {
                        Nombres = "Admin",
                        ApellidoPat = admin.Username,
                        TipoDocumentoId = tipoDocCI?.DominioValorId ?? 1,
                        NumeroDocumento = "00000000",
                        SexoId = sexoIndet?.DominioValorId ?? 1,
                        CorreoPrincipal = admin.Email,
                        Estado = "Activo",
                        FechaRegistro = DateTime.Now
                    };
                    context.Personas.Add(persona);
                    await context.SaveChangesAsync();

                    var usuario = new Usuario
                    {
                        PersonaId = persona.PersonaId,
                        NombreUsuario = admin.Username,
                        Contrasena = BCrypt.Net.BCrypt.HashPassword(admin.Password),
                        Activo = true,
                        FechaCreacion = DateTime.Now
                    };
                    context.Usuarios.Add(usuario);
                    await context.SaveChangesAsync();

                    context.UsuariosRoles.Add(new UsuariosRole {
                        UsuarioId = usuario.UsuarioId,
                        RolId = rolAdmin.RolId,
                        FechaAsignacion = DateTime.Now
                    });
                    await context.SaveChangesAsync();
                    
                    Console.WriteLine($"[OK] Admin {admin.Username} vinculado correctamente.");
                }
                else
                {
                    Console.WriteLine($"[INFO] El admin {admin.Email} ya existe. Saltando...");
                }
            }
        }
    }

    // Clase auxiliar para el mapeo
    public class AdminConfig {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}