using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;
using RefrescosDelValle.Models.ViewModels;

namespace RefrescosDelValle.Controllers
{
    [Authorize(Policy = "Seguridad")]
    public class SeguridadController : Controller
    {
        private readonly AppDbContext _db;

        public SeguridadController(AppDbContext db)
        {
            _db = db;
        }

        // ══════════════════════════════════════════════════════════
        // VISTAS PRINCIPALES
        // ══════════════════════════════════════════════════════════

        public IActionResult Index() => View();

        public async Task<IActionResult> DirectorioUsuarios()
        {
            var usuarios = await _db.Usuarios
                .Include(u => u.Persona)
                .Include(u => u.UsuariosRoles).ThenInclude(ur => ur.Rol)
                .Include(u => u.UsuariosSucursales).ThenInclude(us => us.Sucursal)
                .Select(u => new UsuarioItemViewModel
                {
                    UsuarioID = u.UsuarioId,
                    NombreCompleto = u.Persona.Nombres + " " + u.Persona.ApellidoPat,
                    Email = u.Persona.CorreoPrincipal ?? u.NombreUsuario,
                    Activo = u.Activo,
                    Roles = string.Join(", ", u.UsuariosRoles.Select(ur => ur.Rol.NombreRol)),
                    Sucursal = u.UsuariosSucursales.Select(us => us.Sucursal.NombreSucursal).FirstOrDefault() ?? "Acceso Remoto",
                    Iniciales = (u.Persona.Nombres.Substring(0, 1) + u.Persona.ApellidoPat.Substring(0, 1)).ToUpper()
                })
                .ToListAsync();

            return View(usuarios);
        }

        public async Task<IActionResult> Auditoria()
        {
            var logs = await _db.BitacoraAcciones
                .Include(b => b.Usuario)
                .OrderByDescending(b => b.FechaAccion)
                .Take(100)
                .Select(b => new AuditoriaViewModel
                {
                    Fecha = b.FechaAccion,
                    Usuario = b.Usuario != null ? b.Usuario.NombreUsuario : "Sistema",
                    Operacion = b.Accion ?? "LOG",
                    Tabla = b.Tabla ?? "General",
                    Detalle = $"RegistroID: {b.RegistroId} | IP: {b.DireccionIp ?? "127.0.0.1"}"
                })
                .ToListAsync();

            return View(logs);
        }

        public IActionResult Roles() => View();
        public async Task<IActionResult> Sesiones()
{
    // Obtenemos las sesiones que no han sido cerradas (Activa = true)
    var sesionesActivas = await _db.Sesiones
        .Include(s => s.Usuario)
        .Where(s => s.Activa == true)
        .OrderByDescending(s => s.FechaInicio)
        .ToListAsync();

    return View(sesionesActivas);
}

        // ══════════════════════════════════════════════════════════
        // REGISTRO DE PERSONAL (OPERADORES)
        // ══════════════════════════════════════════════════════════

        public async Task<IActionResult> RegistrarPersonal()
        {
            await PrepararViewBags();
            return View(new RegistroEmpleadoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPersonal(RegistroEmpleadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PrepararViewBags();
                return View(model);
            }

            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                // 1. Obtener IDs de dominio obligatorios (V1.11)
                // Nota: Verifica si en tu AppDbContext es DominioTipoId o DominioTipoID
                var tipoDocCI = await _db.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 10 && d.Descripcion == "Carnet de Identidad");
                var sexoIndet = await _db.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 1 && d.Descripcion == "Indeterminado");
                var estadoEmpActivo = await _db.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 20 && d.Descripcion == "Activo");

                // 2. Crear Ficha de Persona
                var persona = new Persona
                {
                    NumeroDocumento = model.CI,
                    TipoDocumentoId = tipoDocCI?.DominioValorId ?? 1,
                    SexoId = sexoIndet?.DominioValorId ?? 1,
                    Nombres = model.Nombres,
                    ApellidoPat = model.ApellidoPat,
                    CorreoPrincipal = model.Email,
                    Estado = "Activo"
                };
                _db.Personas.Add(persona);
                await _db.SaveChangesAsync();

                // 3. Crear Credenciales de Usuario
                var usuario = new Usuario
                {
                    PersonaId = persona.PersonaId,
                    NombreUsuario = model.Email,
                    Contrasena = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Activo = true
                };
                _db.Usuarios.Add(usuario);
                await _db.SaveChangesAsync();

                // 4. Crear Perfil de Empleado (RRHH)
                var empleado = new Empleado
                {
                    PersonaId = persona.PersonaId,
                    CargoId = model.CargoID,
                    SucursalId = model.SucursalID,
                    DepartamentoId = 1, // Valor por defecto para evitar errores de FK
                    Salario = 0,
                    EstadoEmpleadoId = estadoEmpActivo?.DominioValorId ?? 1,
                    FechaIngreso = DateOnly.FromDateTime(DateTime.Today)
                };
                _db.Empleados.Add(empleado);

                // 5. Asignar Roles Seleccionados
                if (model.RolesSeleccionados != null)
                {
                    foreach (var rolId in model.RolesSeleccionados)
                    {
                        _db.UsuariosRoles.Add(new UsuariosRole
                        {
                            UsuarioId = usuario.UsuarioId,
                            RolId = rolId
                        });
                    }
                }

                // 6. Asignar Sucursal de trabajo
                _db.UsuariosSucursales.Add(new UsuariosSucursale {
                    UsuarioId = usuario.UsuarioId,
                    SucursalId = model.SucursalID
                });

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                TempData["SuccessMsg"] = $"Operador {model.Nombres} inyectado correctamente.";
                return RedirectToAction(nameof(DirectorioUsuarios));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Fallo crítico en la secuencia: " + ex.Message);
                await PrepararViewBags();
                return View(model);
            }
        }

        // ══════════════════════════════════════════════════════════
        // HELPERS
        // ══════════════════════════════════════════════════════════

        private async Task PrepararViewBags()
        {
            ViewBag.Sucursales = await _db.Sucursales
                .Where(s => s.Activo)
                .Select(s => new SelectListItem { Value = s.SucursalId.ToString(), Text = s.NombreSucursal })
                .ToListAsync();

            ViewBag.Cargos = await _db.Cargos
                .Where(c => c.Activo)
                .Select(c => new SelectListItem { Value = c.CargoId.ToString(), Text = c.NombreCargo })
                .ToListAsync();

            ViewBag.RolesDisponibles = await _db.Roles
                .Where(r => r.Activo)
                .Select(r => new SelectListItem { Value = r.RolId.ToString(), Text = r.NombreRol })
                .ToListAsync();
        }

        
    }
}