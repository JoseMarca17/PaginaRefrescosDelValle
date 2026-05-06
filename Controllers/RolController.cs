using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Controllers
{
    [Authorize(Policy = "Seguridad")]
    public class RolController : Controller
    {
        private readonly AppDbContext _context;

        public RolController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Rol/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Permisos = await _context.Permisos
                .Where(p => p.Activo)
                .OrderBy(p => p.Modulo)
                .ThenBy(p => p.Accion)
                .ToListAsync();

            return View("~/Views/Seguridad/RolCreate.cshtml");
        }

        // POST: /Rol/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role rol, int[] permisosSeleccionados)
        {
            if (ModelState.IsValid)
            {
                rol.FechaCreacion = DateTime.Now;
                rol.Activo = true;
                _context.Roles.Add(rol);
                await _context.SaveChangesAsync();

                if (permisosSeleccionados != null && permisosSeleccionados.Length > 0)
                {
                    foreach (var permisoId in permisosSeleccionados)
                    {
                        _context.RolesPermisos.Add(new RolesPermiso
                        {
                            RolId           = rol.RolId,
                            PermisoId       = permisoId,
                            FechaAsignacion = DateTime.Now
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["Success"] = $"Rol '{rol.NombreRol}' creado exitosamente.";
                return RedirectToAction("Roles", "Seguridad");
            }

            ViewBag.Permisos = await _context.Permisos
                .Where(p => p.Activo)
                .OrderBy(p => p.Modulo)
                .ThenBy(p => p.Accion)
                .ToListAsync();

            return View("~/Views/Seguridad/RolCreate.cshtml");
        }

        // GET: /Rol/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var rol = await _context.Roles
                .Include(r => r.RolesPermisos)
                .FirstOrDefaultAsync(r => r.RolId == id);

            if (rol == null) return NotFound();

            ViewBag.Permisos = await _context.Permisos
                .Where(p => p.Activo)
                .OrderBy(p => p.Modulo)
                .ThenBy(p => p.Accion)
                .ToListAsync();

            ViewBag.PermisosDelRol = rol.RolesPermisos
                .Select(rp => rp.PermisoId)
                .ToHashSet();

            return View("~/Views/Seguridad/RolEdit.cshtml", rol);
        }

        // POST: /Rol/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Role rol, int[] permisosSeleccionados)
        {
            if (id != rol.RolId) return NotFound();

            if (ModelState.IsValid)
            {
                var rolExistente = await _context.Roles
                    .Include(r => r.RolesPermisos)
                    .FirstOrDefaultAsync(r => r.RolId == id);

                if (rolExistente == null) return NotFound();

                rolExistente.NombreRol   = rol.NombreRol;
                rolExistente.Descripcion = rol.Descripcion;
                rolExistente.Activo      = rol.Activo;

                _context.RolesPermisos.RemoveRange(rolExistente.RolesPermisos);

                if (permisosSeleccionados != null && permisosSeleccionados.Length > 0)
                {
                    foreach (var permisoId in permisosSeleccionados)
                    {
                        _context.RolesPermisos.Add(new RolesPermiso
                        {
                            RolId           = id,
                            PermisoId       = permisoId,
                            FechaAsignacion = DateTime.Now
                        });
                    }
                }

                await _context.SaveChangesAsync();
                TempData["Success"] = $"Rol '{rolExistente.NombreRol}' actualizado correctamente.";
                return RedirectToAction("Roles", "Seguridad");
            }

            ViewBag.Permisos = await _context.Permisos
                .Where(p => p.Activo)
                .OrderBy(p => p.Modulo)
                .ThenBy(p => p.Accion)
                .ToListAsync();

            ViewBag.PermisosDelRol = permisosSeleccionados?.ToHashSet() ?? new HashSet<int>();
            return View("~/Views/Seguridad/RolEdit.cshtml");
        }

        // POST: /Rol/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _context.Roles
                .Include(r => r.RolesPermisos)
                .Include(r => r.RolesMenus)
                .FirstOrDefaultAsync(r => r.RolId == id);

            if (rol == null) return NotFound();

            rol.Activo = false;
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Rol '{rol.NombreRol}' desactivado correctamente.";
            return RedirectToAction("Roles", "Seguridad");
        }
    }
}