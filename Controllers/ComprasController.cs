using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;
using System.Security.Claims; // Añadido para el manejo de usuarios

namespace RefrescosDelValle.Controllers
{
    public class ComprasController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ComprasController> _logger;

        public ComprasController(AppDbContext context, ILogger<ComprasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ==================== ÍNDICE PRINCIPAL ====================
        public IActionResult Index()
        {
            return View();
        }

        // ==================== ÓRDENES DE COMPRA ====================
        
        // GET: Compras/OrdenesCompra
        public async Task<IActionResult> OrdenesCompra(string? searchString, int? proveedorId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            // MEJORA: Usar AsNoTracking() para consultas de solo lectura (mejora el rendimiento)
            var query = _context.OrdenesCompras
                .AsNoTracking() 
                .Include(o => o.Proveedor)
                .Include(o => o.UsuarioCreador)
                .Include(o => o.RecepcionMercaderia)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToLower();
                query = query.Where(o => o.NumeroOrden.ToLower().Contains(searchString) || 
                                         (o.Proveedor != null && o.Proveedor.RazonSocial.ToLower().Contains(searchString)));
            }
            
            if (proveedorId.HasValue && proveedorId.Value > 0)
            {
                query = query.Where(o => o.ProveedorId == proveedorId.Value);
            }
            
            if (fechaDesde.HasValue)
            {
                query = query.Where(o => o.FechaEmision >= DateOnly.FromDateTime(fechaDesde.Value));
            }
            
            if (fechaHasta.HasValue)
            {
                var fechaHastaFin = fechaHasta.Value.AddDays(1);
                query = query.Where(o => o.FechaEmision <= DateOnly.FromDateTime(fechaHastaFin));
            }
            
            var ordenes = await query
                .OrderByDescending(o => o.FechaEmision)
                .Select(o => new
                {
                    o.OrdenCompraId,
                    o.NumeroOrden,
                    o.ProveedorId,
                    ProveedorNombre = o.Proveedor != null ? o.Proveedor.RazonSocial : "Sin Proveedor",
                    o.FechaEmision,
                    o.FechaEntregaEsp,
                    o.MontoTotal,
                    CantidadRecepciones = o.RecepcionMercaderia != null ? o.RecepcionMercaderia.Count : 0,
                    // MEJORA: Prevenir posibles NullReferenceExceptions
                    TotalRecibido = o.RecepcionMercaderia != null ? o.RecepcionMercaderia.Sum(r => (int?)r.CantidadRecibida ?? 0) : 0 
                })
                .ToListAsync();
            
            ViewBag.Proveedores = new SelectList(await _context.Proveedores
                .AsNoTracking()
                .Where(p => p.Activo)
                .ToListAsync(), "ProveedorId", "RazonSocial");
            
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentProveedor = proveedorId;
            ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
            ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");
            
            return View(ordenes);
        }
        
        // GET: Compras/OrdenesCompra/Details/5
        public async Task<IActionResult> OrdenCompraDetails(int id)
        {
            var orden = await _context.OrdenesCompras
                .AsNoTracking() // MEJORA: Optimización de lectura
                .Include(o => o.Proveedor)
                .Include(o => o.UsuarioCreador)
                .Include(o => o.Contrato)
                .Include(o => o.RecepcionMercaderia)
                    .ThenInclude(r => r.Inventario)
                        .ThenInclude(i => i != null ? i.Producto : null)
                .Include(o => o.RecepcionMercaderia)
                    .ThenInclude(r => r.Almacen)
                .FirstOrDefaultAsync(o => o.OrdenCompraId == id);
            
            if (orden == null) return NotFound();
            
            return View(orden);
        }
        
        // GET: Compras/OrdenesCompra/Create
        public async Task<IActionResult> CrearOrdenCompra()
        {
            await CargarViewBagsParaOrdenes();
            return View();
        }
        
        // POST: Compras/OrdenesCompra/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // MEJORA: Usar Bind para evitar Overposting (seguridad)
        public async Task<IActionResult> CrearOrdenCompra([Bind("ProveedorId,ContratoId,FechaEntregaEsp,MontoTotal,Observaciones")] OrdenesCompra ordenCompra)
        {

            ModelState.Remove(nameof(ordenCompra.NumeroOrden));
    ModelState.Remove(nameof(ordenCompra.FechaEmision));
    
    // También removemos las propiedades de navegación para que no den falso error
    ModelState.Remove("Proveedor");
    ModelState.Remove("Contrato");
    ModelState.Remove("UsuarioCreador");
    ModelState.Remove("RecepcionMercaderia");
            if (ModelState.IsValid)
            {
                try
                {
                    // MEJORA: Autogenerar Número de Orden en el backend para Refrescos Del Valle
                    var ultimaOrden = await _context.OrdenesCompras.OrderByDescending(o => o.OrdenCompraId).FirstOrDefaultAsync();
                    int nuevoNumero = (ultimaOrden?.OrdenCompraId ?? 0) + 1;
                    ordenCompra.NumeroOrden = $"OC-VALLE-{DateTime.Now:yyyyMMdd}-{nuevoNumero:D4}";

                    // TODO: Reemplazar el "1" con el ID del usuario logueado en un futuro
                    // Ejemplo: int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    ordenCompra.UsuarioCreadorId = 1; 
                    ordenCompra.FechaEmision = DateOnly.FromDateTime(DateTime.Now); 
                    
                    _context.Add(ordenCompra);
                    await _context.SaveChangesAsync();
                    
                    TempData["Success"] = "Orden de compra creada exitosamente";
                    return RedirectToAction(nameof(OrdenesCompra));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear orden de compra para Refrescos Del Valle");
                    ModelState.AddModelError("", "Ocurrió un error inesperado al guardar la orden de compra.");
                }
            }
            
            await CargarViewBagsParaOrdenes(ordenCompra.ProveedorId, ordenCompra.ContratoId);
            return View(ordenCompra);
        }
        
        // GET: Compras/OrdenesCompra/Edit/5
        public async Task<IActionResult> EditarOrdenCompra(int id)
        {
            var orden = await _context.OrdenesCompras.FindAsync(id);
            if (orden == null) return NotFound();
            
            await CargarViewBagsParaOrdenes(orden.ProveedorId, orden.ContratoId);
            return View(orden);
        }
        
        // POST: Compras/OrdenesCompra/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarOrdenCompra(int id, [Bind("OrdenCompraId,ProveedorId,ContratoId,FechaEntregaEsp,MontoTotal,Observaciones")] OrdenesCompra ordenModificada)
        {
            if (id != ordenModificada.OrdenCompraId) return NotFound();
            
            ModelState.Remove(nameof(ordenModificada.NumeroOrden));
    ModelState.Remove(nameof(ordenModificada.FechaEmision));
    ModelState.Remove("Proveedor");
    ModelState.Remove("Contrato");
    ModelState.Remove("UsuarioCreador");
    ModelState.Remove("RecepcionMercaderia");

            if (ModelState.IsValid)
            {
                try
                {
                    // MEJORA: Obtener la orden existente y actualizar solo los campos permitidos.
                    // Esto evita que modifiquen el Número de Orden o la Fecha de Emisión original.
                    var ordenExistente = await _context.OrdenesCompras.FindAsync(id);
                    if (ordenExistente == null) return NotFound();
                    
                    ordenExistente.ProveedorId = ordenModificada.ProveedorId;
                    ordenExistente.ContratoId = ordenModificada.ContratoId;
                    ordenExistente.FechaEntregaEsp = ordenModificada.FechaEntregaEsp;
                    ordenExistente.MontoTotal = ordenModificada.MontoTotal;
                    ordenExistente.Observaciones = ordenModificada.Observaciones;
                    
                    await _context.SaveChangesAsync();
                    
                    TempData["Success"] = "Orden de compra actualizada exitosamente";
                    return RedirectToAction(nameof(OrdenesCompra));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenCompraExists(ordenModificada.OrdenCompraId)) return NotFound();
                    else throw;
                }
            }
            
            await CargarViewBagsParaOrdenes(ordenModificada.ProveedorId, ordenModificada.ContratoId);
            return View(ordenModificada);
        }
        
        // POST: Compras/OrdenesCompra/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarOrdenCompra(int id)
        {
            var orden = await _context.OrdenesCompras
                .Include(o => o.RecepcionMercaderia)
                .FirstOrDefaultAsync(o => o.OrdenCompraId == id);
            
            if (orden == null) return NotFound();
            
            if (orden.RecepcionMercaderia != null && orden.RecepcionMercaderia.Any())
            {
                TempData["Error"] = "No se puede eliminar la orden porque ya hemos recibido mercadería asociada a ella.";
                return RedirectToAction(nameof(OrdenesCompra));
            }
            
            _context.OrdenesCompras.Remove(orden);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Orden de compra eliminada exitosamente";
            return RedirectToAction(nameof(OrdenesCompra));
        }

        // ==================== MÉTODOS AUXILIARES ====================
        
        private bool OrdenCompraExists(int id)
        {
            return _context.OrdenesCompras.Any(e => e.OrdenCompraId == id);
        }

        // MEJORA: Método privado para no repetir la lógica de cargar SelectLists
private async Task CargarViewBagsParaOrdenes(int? proveedorId = null, int? contratoId = null)
{
    // Cambiamos "ProveedorID" por "ProveedorId"
    ViewBag.Proveedores = new SelectList(await _context.Proveedores
        .AsNoTracking()
        .Where(p => p.Activo)
        .ToListAsync(), "ProveedorId", "RazonSocial", proveedorId);
    
    // Cambiamos "ContratoID" por "ContratoId"
    ViewBag.Contratos = new SelectList(await _context.Contratos
        .AsNoTracking()
        .Where(c => c.Activo)
        .ToListAsync(), "ContratoId", "NumeroContrato", contratoId);
}

        // ... (Tu código de RECEPCIONES puede seguir abajo, le puedes aplicar principios similares)
    }
}