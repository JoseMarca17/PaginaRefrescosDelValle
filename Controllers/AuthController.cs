using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Data;
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

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Password, usuario.PasswordHash))
            {
                ModelState.AddModelError("", "Credenciales incorrectas o acceso denegado.");
                return View(model);
            }

            TempData["UserEmail"] = usuario.Persona.CorreoPrincipal;
            TempData["UserName"] = $"{usuario.Persona.Nombres} {usuario.Persona.ApellidoPat}";

            var codigo = await _otpService.GenerarYGuardarOTPAsync(usuario.UsuarioID);
            
            if (!string.IsNullOrEmpty(usuario.Persona.CorreoPrincipal))
            {
                await _emailService.EnviarCodigoOTPAsync(usuario.Persona.CorreoPrincipal, codigo);
            }

            return RedirectToAction("Verificar2FA", new { idUsuario = usuario.UsuarioID });
        }

        // ================= GET / POST REGISTRO =================
        public IActionResult Registro() => View();

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroClienteViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Evitar clones en la base de datos
            var existeCI = await _db.Personas.AnyAsync(p => p.CI == model.CI);
            var existeEmail = await _db.Personas.AnyAsync(p => p.CorreoPrincipal == model.Email);

            if (existeCI || existeEmail)
            {
                ModelState.AddModelError("", "Error de integridad: El CI o Correo ya existen en el clúster.");
                return View(model);
            }

            // Iniciamos transacción ACID
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // 1. Insertar Persona
                var persona = new RefrescosDelValle.Models.Entities.Persona
                {
                    CI = model.CI,
                    Nombres = model.Nombres,
                    ApellidoPat = model.ApellidoPat,
                    ApellidoMat = model.ApellidoMat,
                    CorreoPrincipal = model.Email,
                    Estado = "Activo",
                    FechaRegistro = DateTime.Now
                };
                _db.Personas.Add(persona);
                await _db.SaveChangesAsync();

                // 2. Insertar Usuario
                var usuario = new RefrescosDelValle.Models.Entities.Usuario
                {
                    PersonaID = persona.PersonaID,
                    NombreUsuario = model.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };
                _db.Usuarios.Add(usuario);
                await _db.SaveChangesAsync();

                // 3. Asignar Rol de "Cliente" obligatorio
                var rolCliente = await _db.Roles.FirstOrDefaultAsync(r => r.NombreRol == "Cliente");
                if (rolCliente == null)
                {
                    rolCliente = new RefrescosDelValle.Models.Entities.Rol { NombreRol = "Cliente", Activo = true };
                    _db.Roles.Add(rolCliente);
                    await _db.SaveChangesAsync();
                }

                var usuarioRol = new RefrescosDelValle.Models.Entities.UsuarioRol
                {
                    UsuarioID = usuario.UsuarioID,
                    RolID = rolCliente.RolID,
                    FechaAsignacion = DateTime.Now
                };
                _db.UsuariosRoles.Add(usuarioRol);
                await _db.SaveChangesAsync();

                // Hacer commit a la BD
                await transaction.CommitAsync();

                // Usamos TempData para mandar un flag de éxito a la vista de Login
                TempData["RegistroExitoso"] = "true";
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
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
                .FirstOrDefaultAsync(u => u.UsuarioID == model.IdUsuario);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Error de integridad de usuario.");
                return View(model);
            }

            var roles = await _db.UsuariosRoles
                .Include(ur => ur.Rol) 
                .Where(ur => ur.UsuarioID == usuario.UsuarioID)
                .Select(ur => ur.Rol.NombreRol)
                .ToListAsync();

            var rolPrincipal = roles.FirstOrDefault() ?? "Cliente";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioID.ToString()),
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

            // Redirección por roles
            if (roles.Contains("SuperAdmin") || roles.Contains("AdminSeguridad") || roles.Contains("AdminVentas"))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ReenviarOTP([FromBody] ReenviarOTPViewModel model)
        {
            // ... (Tu código de ReenviarOTP se mantiene igual)
            try
            {
                if (model == null || model.IdUsuario <= 0) return Json(new { success = false, message = "Payload corrupto." });
                var usuario = await _db.Usuarios.Include(u => u.Persona).FirstOrDefaultAsync(u => u.UsuarioID == model.IdUsuario);
                if (usuario == null || !usuario.Activo) return Json(new { success = false, message = "Cuenta inaccesible." });
                var nuevoCodigo = await _otpService.GenerarYGuardarOTPAsync(usuario.UsuarioID);
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