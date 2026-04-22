using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Data;
using RefrescosDelValle.Models.Entities;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Controllers
{
    public class RolController : Controller
    {
        private readonly AppDbContext _context;

        public RolController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Rol
        public async Task<IActionResult> Index()
        {
            var roles = await _context.Roles
                .Include(r => r.RolesPermisos)
                    .ThenInclude(rp => rp.Permiso)
                .Include(r => r.RolesMenus)
                    .ThenInclude(rm => rm.Menu)
                .OrderBy(r => r.RolId)
                .ToListAsync();

            return View(roles);
        }

        // GET: /Rol/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Permisos = await _context.Permisos
                .Where(p => p.Activo)
                .OrderBy(p => p.Modulo)
                .ThenBy(p => p.Accion)
                .ToListAsync();

            return View();
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

                // Asignar permisos seleccionados
                if (permisosSeleccionados != null && permisosSeleccionados.Length > 0)
                {
                    foreach (var permisoId in permisosSeleccionados)
                    {
                        _context.RolesPermisos.Add(new RolesPermiso
                        {
                            RolId = rol.RolId,
                            PermisoId = permisoId,
                            FechaAsignacion = DateTime.Now
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                TempData["Success"] = $"Rol '{rol.NombreRol}' creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Permisos = await _context.Permisos
                .Where(p => p.Activo)
                .OrderBy(p => p.Modulo)
                .ToListAsync();

            return View(rol);
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
                .ToListAsync();

            ViewBag.PermisosAsignados = rol.RolesPermisos.Select(rp => rp.PermisoId).ToList();

            return View(rol);
        }

        // POST: /Rol/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Role rol, int[] permisosSeleccionados)
        {
            if (id != rol.RolId) return NotFound();

            if (ModelState.IsValid)
            {
                // Actualizar rol
                var rolExistente = await _context.Roles
                    .Include(r => r.RolesPermisos)
                    .FirstOrDefaultAsync(r => r.RolId == id);

                if (rolExistente == null) return NotFound();

                rolExistente.NombreRol = rol.NombreRol;
                rolExistente.Descripcion = rol.Descripcion;
                rolExistente.Activo = rol.Activo;

                // Actualizar permisos: eliminar los anteriores y agregar los nuevos
                _context.RolesPermisos.RemoveRange(rolExistente.RolesPermisos);

                if (permisosSeleccionados != null && permisosSeleccionados.Length > 0)
                {
                    foreach (var permisoId in permisosSeleccionados)
                    {
                        rolExistente.RolesPermisos.Add(new RolesPermiso
                        {
                            RolId = id,
                            PermisoId = permisoId,
                            FechaAsignacion = DateTime.Now
                        });
                    }
                }

                await _context.SaveChangesAsync();
                TempData["Success"] = $"Rol '{rolExistente.NombreRol}' actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Permisos = await _context.Permisos.Where(p => p.Activo).ToListAsync();
            ViewBag.PermisosAsignados = permisosSeleccionados?.ToList() ?? new List<int>();
            return View(rol);
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

            // Soft delete: marcar como inactivo en lugar de eliminar
            rol.Activo = false;
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Rol '{rol.NombreRol}' desactivado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}