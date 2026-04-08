using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities; // <-- Apunta al nuevo contexto escaneado

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
                return; 
            }

            Console.WriteLine("[SYS] Base de datos limpia. Inyectando Rol SuperAdmin...");

            // 2. Crear el Rol 'SuperAdmin' si no existe
            // Ojo: El scaffolding pudo haber llamado a la tabla "Roles" o "Rols"
            var rolAdmin = await context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "SuperAdmin");
            if (rolAdmin == null)
            {
                rolAdmin = new Role { NombreRol = "SuperAdmin", Activo = true };
                context.Roles.Add(rolAdmin);
                await context.SaveChangesAsync();
            }

            // 3. Extraer credenciales
            string adminEmail = config["AdminSetup:Email"] ?? "marcajose1703@gmail.com";
            string adminPass = config["AdminSetup:Password"] ?? "Jose.17042006";
            string adminUser = config["AdminSetup:Username"] ?? "root";

            Console.WriteLine($"[SYS] Configurando credenciales para: {adminEmail}...");

            // --- MAGIA V1.11: Buscar IDs obligatorios en DominioValor ---
            // Nota: Verifica en tus clases si Entity Framework les llamó "DominioValores" o "DominioValors"
            var tipoDocCI = await context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 10 && d.Descripcion == "Carnet de Identidad");
            var sexoIndet = await context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 1 && d.Descripcion == "Indeterminado");

            // 4. Crear Persona
            var persona = new Persona
            {
                Nombres = "Administrador",
                ApellidoPat = "Sistema",
                TipoDocumentoId = tipoDocCI?.DominioValorId ?? 1, // ID obligatorio
                NumeroDocumento = "00000000",                     // Antes era CI
                SexoId = sexoIndet?.DominioValorId ?? 1,          // ID obligatorio
                CorreoPrincipal = adminEmail,
                Estado = "Activo",
                FechaRegistro = DateTime.Now
            };
            context.Personas.Add(persona);
            await context.SaveChangesAsync();

            // 5. Crear Usuario
            var usuario = new Usuario
            {
                PersonaId = persona.PersonaId,
                NombreUsuario = adminUser,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(adminPass), // Antes era PasswordHash
                Activo = true,
                FechaCreacion = DateTime.Now
            };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            // 6. Enlazar Usuario y Rol
            // Nota: Scaffolding a veces llama a esta clase "UsuariosRole" en vez de "UsuarioRol"
            var usuarioRol = new UsuariosRole 
            {
                UsuarioId = usuario.UsuarioId,
                RolId = rolAdmin.RolId,
                FechaAsignacion = DateTime.Now
            };
            context.UsuariosRoles.Add(usuarioRol);
            await context.SaveChangesAsync();

            Console.WriteLine("[OK] SuperAdmin inyectado con éxito en la Matrix.\n");
        }
    }
}