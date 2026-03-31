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

        // GET /Auth/Login
        public IActionResult Login() => View();

        // POST /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var usuario = await _db.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Activo);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Password, usuario.PasswordHash))
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos");
                return View(model);
            }

            var codigo = await _otpService.GenerarYGuardarOTPAsync(usuario.IdUsuario);
            await _emailService.EnviarCodigoOTPAsync(usuario.Email, codigo);

            return RedirectToAction("Verificar2FA", new { idUsuario = usuario.IdUsuario });
        }

        // GET /Auth/Verificar2FA
        public IActionResult Verificar2FA(int idUsuario)
        {
            return View(new VerificarOTPViewModel { IdUsuario = idUsuario });
        }

        // POST /Auth/Verificar2FA
        [HttpPost]
        public async Task<IActionResult> Verificar2FA(VerificarOTPViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var valido = await _otpService.ValidarOTPAsync(model.IdUsuario, model.Codigo);

            if (!valido)
            {
                ModelState.AddModelError("", "Código incorrecto o expirado");
                return View(model);
            }

            var usuario = await _db.Usuarios.FindAsync(model.IdUsuario);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario!.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToAction("Index", "Home");
        }

        // GET /Auth/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}