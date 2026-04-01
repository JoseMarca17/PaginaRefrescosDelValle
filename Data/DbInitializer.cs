using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            Console.WriteLine("\n[SYS] --- INICIANDO SECUENCIA DE ARRANQUE DE BD ---");

            // 1. Verificar si ya existen usuarios
            if (await context.Usuarios.AnyAsync())
            {
                Console.WriteLine("[WARN] Seeder OMITIDO: Se detectaron usuarios existentes en el Mainframe.");
                
                // Te mostramos qué usuario fantasma está bloqueando el sistema
                var userFantasma = await context.Usuarios.Include(u => u.Persona).FirstOrDefaultAsync();
                if (userFantasma != null)
                {
                    Console.WriteLine($"[INFO] Usuario detectado en BD -> Email: {userFantasma.Persona?.CorreoPrincipal} | Username: {userFantasma.NombreUsuario}");
                }
                return; 
            }

            Console.WriteLine("[SYS] Base de datos limpia. Inyectando Rol SuperAdmin...");

            // 2. Crear el Rol 'SuperAdmin'
            var rolAdmin = await context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "SuperAdmin");
            if (rolAdmin == null)
            {
                rolAdmin = new Rol { NombreRol = "SuperAdmin", Activo = true };
                context.Roles.Add(rolAdmin);
                await context.SaveChangesAsync();
            }

            // 3. Extraer credenciales
            string adminEmail = config["AdminSetup:Email"] ?? "marcajose1703@gmail.com";
            string adminPass = config["AdminSetup:Password"] ?? "Jose.17042006";
            string adminUser = config["AdminSetup:Username"] ?? "root";

            Console.WriteLine($"[SYS] Configurando credenciales para: {adminEmail}...");

            // 4. Crear Persona
            var persona = new Persona
            {
                Nombres = "Administrador",
                ApellidoPat = "Sistema",
                CI = "00000000",
                CorreoPrincipal = adminEmail,
                Estado = "Activo",
                FechaRegistro = DateTime.Now
            };
            context.Personas.Add(persona);
            await context.SaveChangesAsync();

            // 5. Crear Usuario
            var usuario = new Usuario
            {
                PersonaID = persona.PersonaID,
                NombreUsuario = adminUser,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminPass),
                Activo = true,
                FechaCreacion = DateTime.Now
            };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            // 6. Enlazar Usuario y Rol
            var usuarioRol = new UsuarioRol
            {
                UsuarioID = usuario.UsuarioID,
                RolID = rolAdmin.RolID,
                FechaAsignacion = DateTime.Now
            };
            context.UsuariosRoles.Add(usuarioRol);
            await context.SaveChangesAsync();

            Console.WriteLine("[OK] SuperAdmin inyectado con éxito en la Matrix.\n");
        }
    }
}