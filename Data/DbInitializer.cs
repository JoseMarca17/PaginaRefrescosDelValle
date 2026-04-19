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

            // 1. Asegurar que el Rol 'SuperAdmin' exista
            var rolAdmin = await context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "SuperAdmin");
            if (rolAdmin == null)
            {
                rolAdmin = new Role { NombreRol = "SuperAdmin", Activo = true };
                context.Roles.Add(rolAdmin);
                await context.SaveChangesAsync();
                Console.WriteLine("[SYS] Rol 'SuperAdmin' creado.");
            }

            // 2. Obtener lista de Admins desde appsettings.json
            var adminsToCreate = config.GetSection("AdminSetup").Get<List<AdminConfig>>()
                                 ?? new List<AdminConfig>();

            if (!adminsToCreate.Any())
            {
                Console.WriteLine("[WARN] No se encontraron admins en AdminSetup. Revisa appsettings.json.");
                return;
            }

            // 3. IDs Auxiliares
            var tipoDocCI = await context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 10 && d.Descripcion == "Carnet de Identidad");
            var sexoIndet = await context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 1  && d.Descripcion == "Indeterminado");

            int contador = 0;

            foreach (var admin in adminsToCreate.Where(a => !string.IsNullOrEmpty(a.Email)))
            {
                var existePersona = await context.Personas.AnyAsync(p => p.CorreoPrincipal == admin.Email);

                if (!existePersona)
                {
                    Console.WriteLine($"[SYS] Inyectando nuevo admin: {admin.Email}...");

                    // Número de documento único por admin para evitar conflictos de UNIQUE constraint
                    var numDoc = $"ADMIN{contador:D5}";

                    var persona = new Persona
                    {
                        Nombres          = "Admin",
                        ApellidoPat      = admin.Username,
                        TipoDocumentoId  = tipoDocCI?.DominioValorId ?? 1,
                        NumeroDocumento  = numDoc,
                        SexoId           = sexoIndet?.DominioValorId ?? 1,
                        CorreoPrincipal  = admin.Email,
                        Estado           = "Activo",
                        FechaRegistro    = DateTime.Now
                    };
                    context.Personas.Add(persona);
                    await context.SaveChangesAsync(); // Necesario para obtener PersonaId

                    var usuario = new Usuario
                    {
                        PersonaId     = persona.PersonaId,
                        NombreUsuario = admin.Username,
                        Contrasena    = BCrypt.Net.BCrypt.HashPassword(admin.Password),
                        Activo        = true,
                        FechaCreacion = DateTime.Now
                    };
                    context.Usuarios.Add(usuario);
                    await context.SaveChangesAsync(); // Necesario para obtener UsuarioId

                    context.UsuariosRoles.Add(new UsuariosRole
                    {
                        UsuarioId       = usuario.UsuarioId,
                        RolId           = rolAdmin.RolId,
                        FechaAsignacion = DateTime.Now
                    });
                    await context.SaveChangesAsync();

                    Console.WriteLine($"[OK] Admin '{admin.Username}' creado y vinculado correctamente.");
                    contador++;
                }
                else
                {
                    Console.WriteLine($"[INFO] Admin '{admin.Email}' ya existe. Saltando...");
                }
            }

            Console.WriteLine($"[SYS] --- SINCRONIZACIÓN COMPLETADA ({contador} admin(s) nuevos) ---\n");
        }
    }

    public class AdminConfig
    {
        public string Email    { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}