using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;
using System.Linq;

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
        // Se añade soporte para paginación básica y filtros mejorados
        public async Task<IActionResult> Index(string? searchString, bool? activo, int page = 1)
        {
            IQueryable<Proveedore> query = _context.Proveedores
                .Include(p => p.Persona)
                .AsNoTracking(); // Mejora rendimiento para solo lectura

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.Trim();
                query = query.Where(p => p.RazonSocial.Contains(searchString) || 
                                       p.Nit.Contains(searchString) ||
                                       p.Persona.Nombres.Contains(searchString) ||
                                       p.Persona.ApellidoPat.Contains(searchString));
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


// GET: Proveedores/Create
public IActionResult Create()
{
    // Filtrar: Traer solo las personas que NO existen en la tabla Proveedores
    var personasDisponibles = _context.Personas
        .Where(p => !_context.Proveedores.Any(prov => prov.PersonaId == p.PersonaId))
        .Select(p => new 
        {
            Id = p.PersonaId, 
            NombreCompleto = p.Nombres + " " + p.ApellidoPat
        }).ToList();

    ViewData["PersonaId"] = new SelectList(personasDisponibles, "Id", "NombreCompleto");
    
    return View();
}

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var proveedor = await _context.Proveedores
                .Include(p => p.Persona)
                    .ThenInclude(pe => pe.CiudadResidencia)
                .Include(p => p.OrdenesCompras)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProveedorId == id);

            if (proveedor == null) return NotFound();

            // Lógica de resumen financiero para RefrescosDelValle
            var stats = new ProveedorStatsViewModel
            {
                TotalOrdenes = proveedor.OrdenesCompras.Count,
                MontoTotalInvertido = proveedor.OrdenesCompras.Sum(o => o.MontoTotal),
                UltimaCompra = proveedor.OrdenesCompras.OrderByDescending(o => o.FechaEmision).FirstOrDefault()?.FechaEmision,
                Activo = proveedor.Activo
            };

            ViewBag.Stats = stats;
            return View(proveedor);
        }

        // POST: Proveedores/Create
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("PersonaId,RazonSocial,Nit,Observaciones")] Proveedore proveedor)
{
    // 1. EL TRUCO ESTÁ AQUÍ: Ignorar los campos que llenamos manualmente o que son de relación
    ModelState.Remove("Persona");
    ModelState.Remove("OrdenesCompras");
    ModelState.Remove("Contratos");
    ModelState.Remove("FechaRegistro");
    ModelState.Remove("Activo");

    // 2. Validar Nit único (tu lógica original)
    if (!string.IsNullOrEmpty(proveedor.Nit) && 
        await _context.Proveedores.AnyAsync(p => p.Nit == proveedor.Nit))
    {
        ModelState.AddModelError("Nit", "Este NIT ya pertenece a un proveedor registrado.");
    }

    // 3. Validar Razón Social única
    if (!string.IsNullOrEmpty(proveedor.RazonSocial) &&
        await _context.Proveedores.AnyAsync(p => p.RazonSocial == proveedor.RazonSocial))
    {
        ModelState.AddModelError("RazonSocial", "La Razón Social ya está registrada.");
    }

    if (ModelState.IsValid)
    {
        try
        {
            // Asignamos los valores por defecto por código
            proveedor.FechaRegistro = DateTime.Now;
            proveedor.Activo = true;

            _context.Add(proveedor);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = $"El proveedor {proveedor.RazonSocial} ha sido registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
{
    _logger.LogError(ex, "Error al registrar proveedor");
    
    // TRUCO: Extraer el error real de la base de datos
    string errorReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
    
    ModelState.AddModelError("", $"Error exacto de SQL: {errorReal}");
}
    }

    // 4. SI LLEGA AQUÍ ES PORQUE ALGO FALLÓ (Regresa a la vista)
    // NECESITAMOS RECARGAR EL COMBOBOX O DARÁ ERROR
    var personas = _context.Personas.Select(p => new 
    {
        Id = p.PersonaId, 
        NombreCompleto = p.Nombres + " " + p.ApellidoPat
    }).ToList();
    
    ViewData["PersonaId"] = new SelectList(personas, "Id", "NombreCompleto", proveedor.PersonaId);

    return View(proveedor);
}

        // POST: Proveedores/ToggleStatus/5
        // Método optimizado para cambiar el estado sin cargar todo el objeto
        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var proveedor = await _context.Proveedores.Select(p => new { p.ProveedorId, p.Activo, p.RazonSocial })
                                                      .FirstOrDefaultAsync(p => p.ProveedorId == id);
            
            if (proveedor == null) return Json(new { success = false });

            // Actualización atómica del estado
            await _context.Proveedores
                .Where(p => p.ProveedorId == id)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.Activo, p => !p.Activo));

            return Json(new { success = true, nuevoEstado = !proveedor.Activo, nombre = proveedor.RazonSocial });
        }

        // GET: Proveedores/Edit/5
public async Task<IActionResult> Edit(int id)
{
    var proveedor = await _context.Proveedores.FindAsync(id);
    if (proveedor == null)
    {
        return NotFound();
    }

    // EL TRUCO: Traer a las personas libres + la persona actual de ESTE proveedor
    var personasPermitidas = _context.Personas
        .Where(p => !_context.Proveedores.Any(prov => prov.PersonaId == p.PersonaId) 
                 || p.PersonaId == proveedor.PersonaId)
        .Select(p => new 
        {
            Id = p.PersonaId, 
            NombreCompleto = p.Nombres + " " + p.ApellidoPat
        }).ToList();

    ViewData["PersonaId"] = new SelectList(personasPermitidas, "Id", "NombreCompleto", proveedor.PersonaId);
    
    return View(proveedor);
}

// POST: Proveedores/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("ProveedorId,PersonaId,RazonSocial,Nit,Observaciones,Activo")] Proveedore proveedor)
{
    if (id != proveedor.ProveedorId)
    {
        return NotFound();
    }

    // Ignorar campos de navegación y fecha registro (la fecha no se edita)
    ModelState.Remove("Persona");
    ModelState.Remove("OrdenesCompras");
    ModelState.Remove("Contratos");
    ModelState.Remove("FechaRegistro");

    // Validar NIT único (excluyendo a este mismo proveedor)
    if (await _context.Proveedores.AnyAsync(p => p.Nit == proveedor.Nit && p.ProveedorId != id))
        ModelState.AddModelError("Nit", "El NIT ya está siendo usado por otro proveedor.");

    // Validar Razón Social única (excluyendo a este mismo proveedor)
    if (await _context.Proveedores.AnyAsync(p => p.RazonSocial == proveedor.RazonSocial && p.ProveedorId != id))
        ModelState.AddModelError("RazonSocial", "La Razón Social ya está en uso por otro proveedor.");

    // Validar que la persona elegida no esté ocupada por OTRO proveedor
    if (await _context.Proveedores.AnyAsync(p => p.PersonaId == proveedor.PersonaId && p.ProveedorId != id))
        ModelState.AddModelError("PersonaId", "Esta persona ya es representante de otro proveedor.");

    if (ModelState.IsValid)
    {
        try
        {
            // Buscamos el original en la BD para no sobrescribir la FechaRegistro accidentalmente
            var proveedorDB = await _context.Proveedores.FindAsync(id);
            if (proveedorDB == null) return NotFound();

            proveedorDB.PersonaId = proveedor.PersonaId;
            proveedorDB.RazonSocial = proveedor.RazonSocial;
            proveedorDB.Nit = proveedor.Nit;
            proveedorDB.Observaciones = proveedor.Observaciones;
            proveedorDB.Activo = proveedor.Activo;

            await _context.SaveChangesAsync();
            TempData["Success"] = $"El proveedor {proveedor.RazonSocial} fue actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al editar proveedor");
            string errorReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            ModelState.AddModelError("", $"Error al guardar: {errorReal}");
        }
    }

    // SI FALLA: Recargamos la lista
    var personasPermitidas = _context.Personas
        .Where(p => !_context.Proveedores.Any(prov => prov.PersonaId == p.PersonaId) 
                 || p.PersonaId == proveedor.PersonaId)
        .Select(p => new { Id = p.PersonaId, NombreCompleto = p.Nombres + " " + p.ApellidoPat })
        .ToList();
    
    ViewData["PersonaId"] = new SelectList(personasPermitidas, "Id", "NombreCompleto", proveedor.PersonaId);

    return View(proveedor);
}

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedor = await _context.Proveedores
                .Include(p => p.OrdenesCompras)
                .FirstOrDefaultAsync(p => p.ProveedorId == id);

            if (proveedor == null) return NotFound();

            // Regla de integridad: No se borran proveedores con historial de compras
            if (proveedor.OrdenesCompras.Any())
            {
                TempData["Error"] = "No es posible eliminar el proveedor porque existen órdenes de compra registradas. Se recomienda desactivarlo.";
                return RedirectToAction(nameof(Index));
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Proveedor eliminado del catálogo.";
            
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(int id) => _context.Proveedores.Any(e => e.ProveedorId == id);
    }

    // ViewModel auxiliar para simplificar la vista de detalles
    public class ProveedorStatsViewModel
    {
        public int TotalOrdenes { get; set; }
        public decimal MontoTotalInvertido { get; set; }
        public DateOnly? UltimaCompra { get; set; }
        public bool Activo { get; set; }
    }
}