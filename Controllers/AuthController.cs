using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities; 
using RefrescosDelValle.Models.ViewModels;
using RefrescosDelValle.Services;
using System.Security.Claims;

namespace RefrescosDelValle.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _db;
        private readonly EmailService _emailService;
        private readonly OTPService _otpService;

        public AuthController(AppDbContext db, EmailService emailService, OTPService otpService)
        {
            _db = db;
            _emailService = emailService;
            _otpService = otpService;
        }

        // ================= GET / POST LOGIN =================
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = await _db.Usuarios
                .Include(u => u.Persona)
                .FirstOrDefaultAsync(u => 
                    (u.Persona.CorreoPrincipal == model.Email || u.NombreUsuario == model.Email) 
                    && u.Activo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Password, usuario.Contrasena))
            {
                ModelState.AddModelError("", "Credenciales incorrectas o acceso denegado.");
                return View(model);
            }

            TempData["UserEmail"] = usuario.Persona.CorreoPrincipal;
            TempData["UserName"] = $"{usuario.Persona.Nombres} {usuario.Persona.ApellidoPat}";

            var codigo = await _otpService.GenerarYGuardarOTPAsync(usuario.UsuarioId);
            
            if (!string.IsNullOrEmpty(usuario.Persona.CorreoPrincipal))
            {
                await _emailService.EnviarCodigoOTPAsync(usuario.Persona.CorreoPrincipal, codigo);
            }

            return RedirectToAction("Verificar2FA", new { idUsuario = usuario.UsuarioId });
        }

        // ================= GET / POST REGISTRO =================
        public IActionResult Registro() => View();

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroClienteViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Bloquear clones en la base de datos
            var existeCI = await _db.Personas.AnyAsync(p => p.NumeroDocumento == model.CI);
            var existeEmail = await _db.Personas.AnyAsync(p => p.CorreoPrincipal == model.Email);

            if (existeCI || existeEmail)
            {
                ModelState.AddModelError("", "Error de integridad: El CI o Correo ya existen en el clúster.");
                return View(model);
            }

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // Buscar IDs obligatorios
                var tipoDocCI = await _db.Set<DominioValor>().FirstOrDefaultAsync(d => d.DominioTipoId == 10 && d.Descripcion == "Carnet de Identidad");
                var sexoIndet = await _db.Set<DominioValor>().FirstOrDefaultAsync(d => d.DominioTipoId == 1 && d.Descripcion == "Indeterminado");

                // 1. Insertar Persona
                var persona = new Persona
                {
                    NumeroDocumento = model.CI,
                    TipoDocumentoId = tipoDocCI?.DominioValorId ?? 1, 
                    SexoId = sexoIndet?.DominioValorId ?? 1,          
                    Nombres = model.Nombres,
                    ApellidoPat = model.ApellidoPat,
                    ApellidoMat = model.ApellidoMat,
                    CorreoPrincipal = model.Email,
                    Estado = "Activo"
                };
                _db.Personas.Add(persona);
                await _db.SaveChangesAsync();

                // 2. Insertar Usuario
                var usuario = new Usuario
                {
                    PersonaId = persona.PersonaId,
                    NombreUsuario = model.Email,
                    Contrasena = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Activo = true
                };
                _db.Usuarios.Add(usuario);
                await _db.SaveChangesAsync();

                // 3. Asignar Rol de "Cliente"
                var rolCliente = await _db.Set<Role>().FirstOrDefaultAsync(r => r.NombreRol == "Cliente");
                if (rolCliente == null)
                {
                    rolCliente = new Role { NombreRol = "Cliente", Activo = true };
                    _db.Set<Role>().Add(rolCliente);
                    await _db.SaveChangesAsync();
                }

                var usuarioRol = new UsuariosRole
                {
                    UsuarioId = usuario.UsuarioId,
                    RolId = rolCliente.RolId
                };
                _db.Set<UsuariosRole>().Add(usuarioRol);
                await _db.SaveChangesAsync();

                // 4. [EL PARCHE CLAVE] Registrar en la tabla Clientes del ERP
                var tipoClienteMinorista = await _db.Set<TiposCliente>().FirstOrDefaultAsync(t => t.Descripcion == "Minorista");
                var cliente = new Cliente
                {
                    PersonaId = persona.PersonaId,
                    TipoClienteId = tipoClienteMinorista?.TipoClienteId,
                    Activo = true
                };
                _db.Set<Cliente>().Add(cliente);
                await _db.SaveChangesAsync();

                // 5. Commit final
                await transaction.CommitAsync();

                TempData["RegistroExitoso"] = "true";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"[CRITICAL] Fallo en el nodo de registro: {ex.Message}");
                ModelState.AddModelError("", "Fallo crítico al ejecutar el INSERT en el servidor.");
                return View(model);
            }
        }

        // ================= 2FA Y UTILIDADES =================
        public IActionResult Verificar2FA(int idUsuario)
        {
            ViewBag.UserEmail = TempData["UserEmail"];
            ViewBag.UserName = TempData["UserName"];
            return View(new VerificarOTPViewModel { IdUsuario = idUsuario });
        }

        [HttpPost]
        public async Task<IActionResult> Verificar2FA(VerificarOTPViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var valido = await _otpService.ValidarOTPAsync(model.IdUsuario, model.Codigo);
            if (!valido)
            {
                ModelState.AddModelError("", "El token criptográfico es inválido o expiró.");
                return View(model);
            }

            var usuario = await _db.Usuarios
                .Include(u => u.Persona)
                .FirstOrDefaultAsync(u => u.UsuarioId == model.IdUsuario);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Error de integridad de usuario.");
                return View(model);
            }

            var roles = await _db.Set<UsuariosRole>()
                .Include(ur => ur.Rol) 
                .Where(ur => ur.UsuarioId == usuario.UsuarioId)
                .Select(ur => ur.Rol.NombreRol)
                .ToListAsync();

            var rolPrincipal = roles.FirstOrDefault() ?? "Cliente";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Name, $"{usuario.Persona.Nombres} {usuario.Persona.ApellidoPat}"),
                new Claim(ClaimTypes.Email, usuario.Persona.CorreoPrincipal ?? "sin-correo@sistema.local"),
                new Claim(ClaimTypes.Role, rolPrincipal)
            };

            foreach (var rol in roles.Skip(1))
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            await _otpService.LimpiarOTPAsync(model.IdUsuario);

            if (roles.Contains("SuperAdmin") || roles.Contains("AdminSeguridad") || roles.Contains("AdminVentas"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ReenviarOTP([FromBody] ReenviarOTPViewModel model)
        {
            try
            {
                if (model == null || model.IdUsuario <= 0) return Json(new { success = false, message = "Payload corrupto." });
                var usuario = await _db.Usuarios.Include(u => u.Persona).FirstOrDefaultAsync(u => u.UsuarioId == model.IdUsuario);
                if (usuario == null || !usuario.Activo) return Json(new { success = false, message = "Cuenta inaccesible." });
                var nuevoCodigo = await _otpService.GenerarYGuardarOTPAsync(usuario.UsuarioId);
                if (!string.IsNullOrEmpty(usuario.Persona.CorreoPrincipal)) await _emailService.EnviarCodigoOTPAsync(usuario.Persona.CorreoPrincipal, nuevoCodigo);
                return Json(new { success = true, message = "Nuevo token transmitido." });
            }
            catch (Exception) { return Json(new { success = false, message = "Fallo crítico de SMTP." }); }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}