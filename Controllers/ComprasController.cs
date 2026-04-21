using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities; 

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
            var query = _context.OrdenesCompras
                .Include(o => o.Proveedor)
                .Include(o => o.UsuarioCreador)
                .Include(o => o.RecepcionMercaderia)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(o => o.NumeroOrden.Contains(searchString) || 
                                         (o.Proveedor != null && o.Proveedor.RazonSocial != null && o.Proveedor.RazonSocial.Contains(searchString)));
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
                    ProveedorNombre = o.Proveedor != null ? o.Proveedor.RazonSocial : "",
                    o.FechaEmision,
                    o.FechaEntregaEsp,
                    o.MontoTotal,
                    o.RecepcionMercaderia,
                    CantidadRecepciones = o.RecepcionMercaderia != null ? o.RecepcionMercaderia.Count : 0,
                    TotalRecibido = o.RecepcionMercaderia != null ? o.RecepcionMercaderia.Sum(r => r.CantidadRecibida) : 0
                })
                .ToListAsync();
            
            ViewBag.Proveedores = new SelectList(await _context.Proveedores
                .Where(p => p.Activo)
                .ToListAsync(), "ProveedorID", "RazonSocial");
            
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
                .Include(o => o.Proveedor)
                .Include(o => o.UsuarioCreador)
                .Include(o => o.Contrato)
                .Include(o => o.RecepcionMercaderia)
                    .ThenInclude(r => r.Inventario)
                        .ThenInclude(i => i != null ? i.Producto : null)
                .Include(o => o.RecepcionMercaderia)
                    .ThenInclude(r => r.Almacen)
                .FirstOrDefaultAsync(o => o.OrdenCompraId == id);
            
            if (orden == null)
            {
                return NotFound();
            }
            
            return View(orden);
        }
        
        // GET: Compras/OrdenesCompra/Create
        public async Task<IActionResult> CrearOrdenCompra()
        {
            ViewBag.Proveedores = new SelectList(await _context.Proveedores
                .Where(p => p.Activo)
                .ToListAsync(), "ProveedorID", "RazonSocial");
            
            ViewBag.Contratos = new SelectList(await _context.Contratos
                .Where(c => c.Activo)
                .ToListAsync(), "ContratoID", "NumeroContrato");
            
            var ultimaOrden = await _context.OrdenesCompras
                .OrderByDescending(o => o.OrdenCompraId)
                .FirstOrDefaultAsync();
            
            int nuevoNumero = (ultimaOrden?.OrdenCompraId ?? 0) + 1;
            ViewBag.NumeroOrdenAuto = $"OC-{DateTime.Now:yyyyMMdd}-{nuevoNumero:D4}";
            
            return View();
        }
        
        // POST: Compras/OrdenesCompra/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearOrdenCompra(OrdenesCompra ordenCompra)
        {
            if (await _context.OrdenesCompras.AnyAsync(o => o.NumeroOrden == ordenCompra.NumeroOrden))
            {
                ModelState.AddModelError("NumeroOrden", "El número de orden ya existe");
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    ordenCompra.UsuarioCreadorId = 1;
                    ordenCompra.FechaEmision = DateOnly.FromDateTime(DateTime.Now); 
                    
                    _context.Add(ordenCompra);
                    await _context.SaveChangesAsync();
                    
                    TempData["Success"] = "Orden de compra creada exitosamente";
                    return RedirectToAction(nameof(OrdenesCompra));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear orden de compra");
                    ModelState.AddModelError("", "Error al guardar la orden de compra");
                }
            }
            
            ViewBag.Proveedores = new SelectList(await _context.Proveedores
                .Where(p => p.Activo)
                .ToListAsync(), "ProveedorID", "RazonSocial", ordenCompra.ProveedorId);
            
            ViewBag.Contratos = new SelectList(await _context.Contratos
                .Where(c => c.Activo)
                .ToListAsync(), "ContratoID", "NumeroContrato", ordenCompra.ContratoId);
            
            return View(ordenCompra);
        }
        
        // GET: Compras/OrdenesCompra/Edit/5
        public async Task<IActionResult> EditarOrdenCompra(int id)
        {
            var orden = await _context.OrdenesCompras.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            
            ViewBag.Proveedores = new SelectList(await _context.Proveedores
                .Where(p => p.Activo)
                .ToListAsync(), "ProveedorID", "RazonSocial", orden.ProveedorId);
            
            ViewBag.Contratos = new SelectList(await _context.Contratos
                .Where(c => c.Activo)
                .ToListAsync(), "ContratoID", "NumeroContrato", orden.ContratoId);
            
            return View(orden);
        }
        
        // POST: Compras/OrdenesCompra/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarOrdenCompra(int id, OrdenesCompra ordenCompra)
        {
            if (id != ordenCompra.OrdenCompraId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    var ordenExistente = await _context.OrdenesCompras.FindAsync(id);
                    if (ordenExistente == null)
                    {
                        return NotFound();
                    }
                    
                    ordenExistente.ProveedorId = ordenCompra.ProveedorId;
                    ordenExistente.ContratoId = ordenCompra.ContratoId;
                    ordenExistente.FechaEntregaEsp = ordenCompra.FechaEntregaEsp;
                    ordenExistente.MontoTotal = ordenCompra.MontoTotal;
                    ordenExistente.Observaciones = ordenCompra.Observaciones;
                    
                    await _context.SaveChangesAsync();
                    
                    TempData["Success"] = "Orden de compra actualizada exitosamente";
                    return RedirectToAction(nameof(OrdenesCompra));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenCompraExists(ordenCompra.OrdenCompraId))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            
            ViewBag.Proveedores = new SelectList(await _context.Proveedores
                .Where(p => p.Activo)
                .ToListAsync(), "ProveedorID", "RazonSocial", ordenCompra.ProveedorId);
            
            ViewBag.Contratos = new SelectList(await _context.Contratos
                .Where(c => c.Activo)
                .ToListAsync(), "ContratoID", "NumeroContrato", ordenCompra.ContratoId);
            
            return View(ordenCompra);
        }
        
        // POST: Compras/OrdenesCompra/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarOrdenCompra(int id)
        {
            var orden = await _context.OrdenesCompras
                .Include(o => o.RecepcionMercaderia)
                .FirstOrDefaultAsync(o => o.OrdenCompraId == id);
            
            if (orden == null)
            {
                return NotFound();
            }
            
            if (orden.RecepcionMercaderia != null && orden.RecepcionMercaderia.Any())
            {
                TempData["Error"] = "No se puede eliminar la orden porque tiene recepciones asociadas";
                return RedirectToAction(nameof(OrdenesCompra));
            }
            
            _context.OrdenesCompras.Remove(orden);
            await _context.SaveChangesAsync();
            
            TempData["Success"] = "Orden de compra eliminada exitosamente";
            return RedirectToAction(nameof(OrdenesCompra));
        }
        
        // ==================== RECEPCIONES ====================
        
        // GET: Compras/Recepciones
        public async Task<IActionResult> Recepciones(int? ordenCompraId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var query = _context.RecepcionMercaderia
                .Include(r => r.OrdenCompra)
                .Include(r => r.Inventario)
                    .ThenInclude(i => i != null ? i.Producto : null)
                .Include(r => r.Almacen)
                .Include(r => r.Usuario)
                .AsQueryable();
            
            if (ordenCompraId.HasValue && ordenCompraId.Value > 0)
            {
                query = query.Where(r => r.OrdenCompraId == ordenCompraId.Value);
            }
            
            if (fechaDesde.HasValue)
            {
                query = query.Where(r => r.FechaRecepcion >= DateOnly.FromDateTime(fechaDesde.Value));
            }
            
            if (fechaHasta.HasValue)
            {
                var fechaHastaFin = fechaHasta.Value.AddDays(1);
                query = query.Where(r => r.FechaRecepcion < DateOnly.FromDateTime(fechaHastaFin));
            }
            
            var recepciones = await query
                .OrderByDescending(r => r.FechaRecepcion)
                .ToListAsync();
            
            ViewBag.OrdenesCompra = new SelectList(await _context.OrdenesCompras
                .OrderByDescending(o => o.FechaEmision)
                .ToListAsync(), "OrdenCompraID", "NumeroOrden");
            
            ViewBag.CurrentOrdenCompra = ordenCompraId;
            ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
            ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");
            
            return View(recepciones);
        }
        
        // GET: Compras/Recepciones/Create
        public async Task<IActionResult> CrearRecepcion(int? ordenCompraId)
        {
            ViewBag.OrdenesCompra = new SelectList(await _context.OrdenesCompras
                .Include(o => o.Proveedor)
                .Where(o => o.RecepcionMercaderia == null || o.RecepcionMercaderia.Sum(r => r.CantidadRecibida) < o.MontoTotal)
                .ToListAsync(), "OrdenCompraId", "NumeroOrden", ordenCompraId);
            
            ViewBag.Almacenes = new SelectList(await _context.Almacens
                .ToListAsync(), "AlmacenId", "NombreAlmacen");
            
            ViewBag.Productos = new SelectList(await _context.Productos
                .Where(p => p.Activo)
                .ToListAsync(), "ProductoId", "NombreProducto");
            
            return View();
        }
        
        // POST: Compras/Recepciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearRecepcion(RecepcionMercaderium recepcion, int productoId, string? lote, DateTime? fechaVencimiento, decimal costoUnitario)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                
                try
                {
                    var inventario = new Inventario
                    {
                        ProductoId = productoId,
                        Lote = lote,
                        FechaIngreso = recepcion.FechaRecepcion,
                        CostoUnitario = costoUnitario,
                        PrecioVenta = costoUnitario * 1.3m,
                        FechaRegistro = DateTime.Now
                    };
                    
                    _context.Inventarios.Add(inventario);
                    await _context.SaveChangesAsync();
                    
                    recepcion.InventarioId = inventario.InventarioId;
                    recepcion.FechaRecepcion = DateOnly.FromDateTime(DateTime.Now);
                    recepcion.UsuarioId = 1;
                    
                    _context.RecepcionMercaderia.Add(recepcion);
                    await _context.SaveChangesAsync();
                    
                    var contenido = await _context.Contenidos
                        .FirstOrDefaultAsync(c => c.AlmacenId == recepcion.AlmacenId && 
                                                   c.InventarioId == inventario.InventarioId);
                    
                    if (contenido == null)
                    {
                        contenido = new Contenido
                        {
                            AlmacenId = recepcion.AlmacenId,
                            ProductoId = productoId,
                            InventarioId = inventario.InventarioId,
                            CantidadDisponible = recepcion.CantidadRecibida,
                            CantidadMinima = 0,
                            EstadoContenidoId = 1,
                            FechaActualizacion = DateTime.Now
                        };
                        _context.Contenidos.Add(contenido);
                    }
                    else
                    {
                        contenido.CantidadDisponible += recepcion.CantidadRecibida;
                        contenido.FechaActualizacion = DateTime.Now;
                    }
                    
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    TempData["Success"] = "Recepción registrada exitosamente";
                    return RedirectToAction(nameof(Recepciones));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al registrar recepción");
                    ModelState.AddModelError("", "Error al registrar la recepción");
                }
            }
            
            ViewBag.OrdenesCompra = new SelectList(await _context.OrdenesCompras
                .ToListAsync(), "OrdenCompraId", "NumeroOrden", recepcion.OrdenCompraId);
            
            ViewBag.Almacenes = new SelectList(await _context.Almacens
                .ToListAsync(), "AlmacenID", "NombreAlmacen", recepcion.AlmacenId);
            
            ViewBag.Productos = new SelectList(await _context.Productos
                .Where(p => p.Activo)
                .ToListAsync(), "ProductoID", "NombreProducto", productoId);
            
            return View(recepcion);
        }
        
        // GET: Compras/Recepciones/Details/5
        public async Task<IActionResult> RecepcionDetails(int id)
        {
            var recepcion = await _context.RecepcionMercaderia
                .Include(r => r.OrdenCompra)
                    .ThenInclude(o => o != null ? o.Proveedor : null)
                .Include(r => r.Inventario)
                    .ThenInclude(i => i != null ? i.Producto : null)
                .Include(r => r.Almacen)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.RecepcionId == id);
            
            if (recepcion == null)
            {
                return NotFound();
            }
            
            return View(recepcion);
        }
        
        // POST: Compras/Recepciones/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarRecepcion(int id)
        {
            var recepcion = await _context.RecepcionMercaderia
                .Include(r => r.Inventario)
                .FirstOrDefaultAsync(r => r.RecepcionId == id);
            
            if (recepcion == null)
            {
                return NotFound();
            }
            
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                if (recepcion.Inventario != null)
                {
                    var contenido = await _context.Contenidos
                        .FirstOrDefaultAsync(c => c.InventarioId == recepcion.InventarioId);
                    
                    if (contenido != null)
                    {
                        _context.Contenidos.Remove(contenido);
                    }
                    
                    _context.Inventarios.Remove(recepcion.Inventario);
                }
                
                _context.RecepcionMercaderia.Remove(recepcion);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                
                TempData["Success"] = "Recepción eliminada exitosamente";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error al eliminar recepción");
                TempData["Error"] = "Error al eliminar la recepción";
            }
            
            return RedirectToAction(nameof(Recepciones));
        }
        
        // ==================== MÉTODOS AUXILIARES ====================
        
        private bool OrdenCompraExists(int id)
        {
            return _context.OrdenesCompras.Any(e => e.OrdenCompraId == id);
        }
        
        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.ProveedorId == id);
        }
    }
}