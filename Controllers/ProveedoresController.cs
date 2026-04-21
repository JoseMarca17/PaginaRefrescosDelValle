using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProveedoresController> _logger;

        public ProveedoresController(AppDbContext context, ILogger<ProveedoresController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index(string? searchString, bool? activo)
        {
            var query = _context.Proveedores
                .Include(p => p.Persona)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => (p.RazonSocial != null && p.RazonSocial.Contains(searchString)) ||
                                         (p.Nit != null && p.Nit.Contains(searchString)) ||
                                         (p.Persona != null && p.Persona.Nombres.Contains(searchString)));
            }

            if (activo.HasValue)
            {
                query = query.Where(p => p.Activo == activo.Value);
            }

            var proveedores = await query
                .OrderBy(p => p.RazonSocial)
                .ToListAsync();

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentActivo = activo;

            return View(proveedores);
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var proveedor = await _context.Proveedores
                .Include(p => p.Persona)
                .Include(p => p.OrdenesCompras)
                    .ThenInclude(o => o.RecepcionMercaderia)
                .Include(p => p.Contratos)
                .FirstOrDefaultAsync(p => p.ProveedorId == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            // Calcular estadísticas
            ViewBag.TotalOrdenes = proveedor.OrdenesCompras?.Count ?? 0;
            ViewBag.TotalRecepciones = proveedor.OrdenesCompras?.Sum(o => o.RecepcionMercaderia?.Count ?? 0) ?? 0;
            ViewBag.TotalMontoCompras = proveedor.OrdenesCompras?.Sum(o => o.MontoTotal) ?? 0;
            ViewBag.ContratosActivos = proveedor.Contratos?.Count(c => c.Activo) ?? 0;

            return View(proveedor);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proveedore proveedor)
        {
            // Validar Nit único
            if (!string.IsNullOrEmpty(proveedor.Nit) && 
                await _context.Proveedores.AnyAsync(p => p.Nit == proveedor.Nit))
            {
                ModelState.AddModelError("Nit", "El Nit ya está registrado");
            }

            // Validar Razón Social única
            if (!string.IsNullOrEmpty(proveedor.RazonSocial) &&
                await _context.Proveedores.AnyAsync(p => p.RazonSocial == proveedor.RazonSocial))
            {
                ModelState.AddModelError("RazonSocial", "La Razón Social ya está registrada");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    proveedor.FechaRegistro = DateTime.Now;
                    proveedor.Activo = true;

                    _context.Add(proveedor);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Proveedor creado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear proveedor");
                    ModelState.AddModelError("", "Error al guardar el proveedor");
                }
            }

            return View(proveedor);
        }

        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proveedore proveedor)
        {
            if (id != proveedor.ProveedorId)
            {
                return NotFound();
            }

            // Validar Nit único (excluyendo el actual)
            if (!string.IsNullOrEmpty(proveedor.Nit) && 
                await _context.Proveedores.AnyAsync(p => p.Nit == proveedor.Nit && p.ProveedorId != id))
            {
                ModelState.AddModelError("Nit", "El Nit ya está registrado por otro proveedor");
            }

            // Validar Razón Social única (excluyendo el actual)
            if (!string.IsNullOrEmpty(proveedor.RazonSocial) &&
                await _context.Proveedores.AnyAsync(p => p.RazonSocial == proveedor.RazonSocial && p.ProveedorId != id))
            {
                ModelState.AddModelError("RazonSocial", "La Razón Social ya está registrada por otro proveedor");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var proveedorExistente = await _context.Proveedores.FindAsync(id);
                    if (proveedorExistente == null)
                    {
                        return NotFound();
                    }

                    proveedorExistente.RazonSocial = proveedor.RazonSocial;
                    proveedorExistente.Nit = proveedor.Nit;
                    proveedorExistente.Observaciones = proveedor.Observaciones;
                    proveedorExistente.Activo = proveedor.Activo;
                    proveedorExistente.PersonaId = proveedor.PersonaId;

                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Proveedor actualizado exitosamente";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.ProveedorId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al actualizar proveedor");
                    ModelState.AddModelError("", "Error al actualizar el proveedor");
                }
            }

            return View(proveedor);
        }

        // POST: Proveedores/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var proveedor = await _context.Proveedores
                .Include(p => p.OrdenesCompras)
                .FirstOrDefaultAsync(p => p.ProveedorId == id);

            if (proveedor == null)
            {
                return NotFound();
            }

            // Verificar si tiene órdenes de compra asociadas
            if (proveedor.OrdenesCompras != null && proveedor.OrdenesCompras.Any())
            {
                TempData["Error"] = "No se puede eliminar el proveedor porque tiene órdenes de compra asociadas";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Proveedores.Remove(proveedor);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Proveedor eliminado exitosamente";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar proveedor");
                TempData["Error"] = "Error al eliminar el proveedor";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Proveedores/ToggleStatus/5
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return Json(new { success = false, message = "Proveedor no encontrado" });
            }

            proveedor.Activo = !proveedor.Activo;
            await _context.SaveChangesAsync();

            return Json(new { success = true, activo = proveedor.Activo, nombre = proveedor.RazonSocial });
        }

        // GET: Proveedores/GetOrdenesCompra/5
        public async Task<IActionResult> GetOrdenesCompra(int id, int? year)
        {
            var query = _context.OrdenesCompras
                .Include(o => o.RecepcionMercaderia)
                .Where(o => o.ProveedorId == id);

            if (year.HasValue && year.Value > 0)
            {
                query = query.Where(o => o.FechaEmision.Year == year.Value);
            }

            var ordenes = await query
                .OrderByDescending(o => o.FechaEmision)
                .Select(o => new
                {
                    o.OrdenCompraId,
                    o.NumeroOrden,
                    o.FechaEmision,
                    o.MontoTotal,
                    CantidadRecepciones = o.RecepcionMercaderia != null ? o.RecepcionMercaderia.Count : 0,
                    TotalRecibido = o.RecepcionMercaderia != null ? o.RecepcionMercaderia.Sum(r => r.CantidadRecibida) : 0
                })
                .ToListAsync();

            return Json(ordenes);
        }

        // GET: Proveedores/ExportToExcel
        public async Task<IActionResult> ExportToExcel()
        {
            var proveedores = await _context.Proveedores
                .OrderBy(p => p.RazonSocial)
                .Select(p => new
                {
                    p.ProveedorId,
                    p.RazonSocial,
                    p.Nit,
                    p.Activo,
                    p.FechaRegistro,
                    p.Observaciones
                })
                .ToListAsync();

            // Aquí puedes implementar la exportación a Excel
            // Por ahora retornamos un JSON
            return Json(proveedores);
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.ProveedorId == id);
        }
    }
}