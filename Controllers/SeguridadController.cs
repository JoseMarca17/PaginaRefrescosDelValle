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

        // ══════════════════════════════════════════════════════════
// EDITAR USUARIO
// ══════════════════════════════════════════════════════════

// GET: /Seguridad/EditarUsuario/5
public async Task<IActionResult> EditarUsuario(int id)
{
    var usuario = await _db.Usuarios
        .Include(u => u.Persona)
        .Include(u => u.UsuariosRoles)
        .FirstOrDefaultAsync(u => u.UsuarioId == id);

    if (usuario == null) return NotFound();

    var vm = new EditarUsuarioViewModel
    {
        UsuarioId      = usuario.UsuarioId,
        Nombres        = usuario.Persona.Nombres,
        ApellidoPat    = usuario.Persona.ApellidoPat,
        ApellidoMat    = usuario.Persona.ApellidoMat,
        CorreoPrincipal= usuario.Persona.CorreoPrincipal,
        TelefonoPrincipal = usuario.Persona.TelefonoPrincipal,
        NombreUsuario  = usuario.NombreUsuario,
        Activo         = usuario.Activo,
        RolesSeleccionados = usuario.UsuariosRoles.Select(ur => ur.RolId).ToList(),
        Iniciales      = (usuario.Persona.Nombres.Substring(0,1) + usuario.Persona.ApellidoPat.Substring(0,1)).ToUpper()
    };

    ViewBag.RolesDisponibles = await _db.Roles
        .Where(r => r.Activo)
        .ToListAsync();

    return View(vm);
}

// POST: /Seguridad/EditarUsuario/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> EditarUsuario(EditarUsuarioViewModel vm)
{
    if (!ModelState.IsValid)
    {
        ViewBag.RolesDisponibles = await _db.Roles.Where(r => r.Activo).ToListAsync();
        return View(vm);
    }

    var usuario = await _db.Usuarios
        .Include(u => u.Persona)
        .Include(u => u.UsuariosRoles)
        .FirstOrDefaultAsync(u => u.UsuarioId == vm.UsuarioId);

    if (usuario == null) return NotFound();

    // Actualizar Persona
    usuario.Persona.Nombres         = vm.Nombres;
    usuario.Persona.ApellidoPat     = vm.ApellidoPat;
    usuario.Persona.ApellidoMat     = vm.ApellidoMat;
    usuario.Persona.CorreoPrincipal = vm.CorreoPrincipal;
    usuario.Persona.TelefonoPrincipal = vm.TelefonoPrincipal;

    // Actualizar Usuario
    usuario.NombreUsuario = vm.NombreUsuario;
    usuario.Activo        = vm.Activo;

    // Cambiar contraseña solo si viene algo
    if (!string.IsNullOrWhiteSpace(vm.NuevaContrasena))
        usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(vm.NuevaContrasena);

    // Reemplazar roles
    _db.UsuariosRoles.RemoveRange(usuario.UsuariosRoles);
    foreach (var rolId in vm.RolesSeleccionados)
    {
        _db.UsuariosRoles.Add(new UsuariosRole
        {
            UsuarioId = usuario.UsuarioId,
            RolId     = rolId
        });
    }

    await _db.SaveChangesAsync();
    TempData["SuccessMsg"] = $"Usuario {usuario.NombreUsuario} actualizado correctamente.";
    return RedirectToAction(nameof(DirectorioUsuarios));
}

// ══════════════════════════════════════════════════════════
// DESACTIVAR / REACTIVAR USUARIO
// ══════════════════════════════════════════════════════════

// POST: /Seguridad/ToggleUsuario/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ToggleUsuario(int id)
{
    var usuario = await _db.Usuarios.FindAsync(id);
    if (usuario == null) return NotFound();

    usuario.Activo = !usuario.Activo;
    await _db.SaveChangesAsync();

    var estado = usuario.Activo ? "reactivado" : "desactivado";
    TempData["SuccessMsg"] = $"Usuario {usuario.NombreUsuario} {estado} correctamente.";
    return RedirectToAction(nameof(DirectorioUsuarios));
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

        public async Task<IActionResult> Roles()
{
    var roles = await _db.Roles
        .Include(r => r.RolesPermisos)
            .ThenInclude(rp => rp.Permiso)
        .Include(r => r.RolesMenus)
            .ThenInclude(rm => rm.Menu)
        .OrderBy(r => r.RolId)
        .ToListAsync();

    return View(roles);
}
// GET: /Seguridad/RolCreate
public async Task<IActionResult> RolCreate()
{
    ViewBag.Permisos = await _db.Permisos
        .Where(p => p.Activo)
        .OrderBy(p => p.Modulo)
        .ThenBy(p => p.Accion)
        .ToListAsync();

    return View();
}

// POST: /Seguridad/RolCreate
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> RolCreate(Role rol, int[] permisosSeleccionados)
{
    if (ModelState.IsValid)
    {
        rol.FechaCreacion = DateTime.Now;
        rol.Activo = true;
        _db.Roles.Add(rol);
        await _db.SaveChangesAsync();

        if (permisosSeleccionados != null && permisosSeleccionados.Length > 0)
        {
            foreach (var permisoId in permisosSeleccionados)
            {
                _db.RolesPermisos.Add(new RolesPermiso
                {
                    RolId = rol.RolId,
                    PermisoId = permisoId,
                    FechaAsignacion = DateTime.Now
                });
            }
            await _db.SaveChangesAsync();
        }

        TempData["Success"] = $"Rol '{rol.NombreRol}' creado exitosamente.";
        return RedirectToAction("Roles");
    }

    ViewBag.Permisos = await _db.Permisos
        .Where(p => p.Activo)
        .OrderBy(p => p.Modulo)
        .ToListAsync();

    return View(rol);
}
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
// AÑADIR ESTAS ACCIONES AL SeguridadController
// (o crear un GeoController separado con la misma estructura)
// ══════════════════════════════════════════════════════════

// GET: /Seguridad/Sucursales
public async Task<IActionResult> Sucursales()
{
    var sucursales = await _db.Sucursales
        .Include(s => s.Ciudad)
            .ThenInclude(c => c.DepartamentoGeo)
        .OrderBy(s => s.Ciudad.DepartamentoGeo.NombreDpto)
        .ThenBy(s => s.Ciudad.NombreCiudad)
        .ThenBy(s => s.NombreSucursal)
        .ToListAsync();

    var departamentos = await _db.Departamentos
        .Include(d => d.Ciudades)
            .ThenInclude(c => c.Sucursales)
        .Include(d => d.Ciudades)
            .ThenInclude(c => c.Zonas)
        .OrderBy(d => d.NombreDpto)
        .ToListAsync();

    var vm = new GeoResumenViewModel
    {
        TotalSucursales   = sucursales.Count,
        TotalActivas      = sucursales.Count(s => s.Activo),
        TotalCiudades     = sucursales.Select(s => s.CiudadId).Distinct().Count(),
        TotalDepartamentos = departamentos.Count,
        Sucursales = sucursales.Select(s => new SucursalItemViewModel
        {
            SucursalId       = s.SucursalId,
            NombreSucursal   = s.NombreSucursal,
            Direccion        = s.Direccion,
            Telefono         = s.Telefono,
            Activo           = s.Activo,
            FechaCreacion    = s.FechaCreacion,
            NombreCiudad     = s.Ciudad.NombreCiudad,
            NombreDepartamento = s.Ciudad.DepartamentoGeo.NombreDpto,
            TotalEmpleados   = s.UsuariosSucursales != null ? s.UsuariosSucursales.Count : 0
        }).ToList(),
        Departamentos = departamentos.Select(d => new DepartamentoResumenViewModel
        {
            DepartamentoGeoId = d.DepartamentoGeoId,
            NombreDpto        = d.NombreDpto,
            Ciudades = d.Ciudades.Select(c => new CiudadResumenViewModel
            {
                CiudadId        = c.CiudadId,
                NombreCiudad    = c.NombreCiudad,
                TotalSucursales = c.Sucursales.Count,
                TotalZonas      = c.Zonas.Count
            }).OrderBy(c => c.NombreCiudad).ToList()
        }).ToList()
    };

    return View(vm);
}

// POST: /Seguridad/CrearSucursal
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> CrearSucursal(CrearSucursalViewModel model)
{
    if (!ModelState.IsValid)
    {
        TempData["ErrorMsg"] = "Datos inválidos. Verifica el formulario.";
        return RedirectToAction(nameof(Sucursales));
    }

    _db.Sucursales.Add(new Sucursale
    {
        NombreSucursal = model.NombreSucursal,
        CiudadId       = model.CiudadId,
        Direccion      = model.Direccion,
        Telefono       = model.Telefono,
        Activo         = true,
        FechaCreacion  = DateTime.Now
    });

    await _db.SaveChangesAsync();
    TempData["SuccessMsg"] = $"Sucursal '{model.NombreSucursal}' creada correctamente.";
    return RedirectToAction(nameof(Sucursales));
}

// POST: /Seguridad/ToggleSucursal/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ToggleSucursal(int id)
{
    var sucursal = await _db.Sucursales.FindAsync(id);
    if (sucursal == null) return NotFound();

    sucursal.Activo = !sucursal.Activo;
    await _db.SaveChangesAsync();

    var estado = sucursal.Activo ? "reactivada" : "desactivada";
    TempData["SuccessMsg"] = $"Sucursal '{sucursal.NombreSucursal}' {estado}.";
    return RedirectToAction(nameof(Sucursales));
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