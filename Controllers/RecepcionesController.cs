using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Controllers
{
    public class RecepcionesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RecepcionesController> _logger;

        public RecepcionesController(AppDbContext context, ILogger<RecepcionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Recepciones
        public async Task<IActionResult> Index(int? ordenCompraId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var query = _context.RecepcionMercaderia
                .Include(r => r.OrdenCompra)
                    .ThenInclude(o => o != null ? o.Proveedor : null)
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
                var fechaDesdeDate = DateOnly.FromDateTime(fechaDesde.Value);
                query = query.Where(r => r.FechaRecepcion >= fechaDesdeDate);
            }

            if (fechaHasta.HasValue)
            {
                var fechaHastaDate = DateOnly.FromDateTime(fechaHasta.Value);
                query = query.Where(r => r.FechaRecepcion <= fechaHastaDate);
            }

            var recepciones = await query
                .OrderByDescending(r => r.FechaRecepcion)
                .ToListAsync();

            ViewBag.OrdenesCompra = new SelectList(await _context.OrdenesCompras
                .OrderByDescending(o => o.FechaEmision)
                .Select(o => new { o.OrdenCompraId, o.NumeroOrden })
                .ToListAsync(), "OrdenCompraID", "NumeroOrden", ordenCompraId);

            ViewBag.CurrentOrdenCompra = ordenCompraId;
            ViewBag.FechaDesde = fechaDesde?.ToString("yyyy-MM-dd");
            ViewBag.FechaHasta = fechaHasta?.ToString("yyyy-MM-dd");

            return View(recepciones);
        }

        // GET: Recepciones/Details/5
        public async Task<IActionResult> Details(int id)
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

        // GET: Recepciones/Create
        public async Task<IActionResult> Create(int? ordenCompraId)
        {
            // Obtener órdenes de compra pendientes o parciales
            var ordenesDisponibles = await _context.OrdenesCompras
                .Include(o => o.Proveedor)
                .Include(o => o.RecepcionMercaderia)
                .Where(o => o.CuentasPagars.Count == 0 )
                .ToListAsync();

            // Filtrar órdenes que aún tienen saldo pendiente
            var ordenesConSaldo = ordenesDisponibles
                .Where(o => o.RecepcionMercaderia == null || o.RecepcionMercaderia.Sum(r => r.CantidadRecibida) < o.MontoTotal)
                .Select(o => new { 
                    o.OrdenCompraId, 
                    Display = $"{o.NumeroOrden} - {(o.Proveedor != null ? o.Proveedor.RazonSocial : "")} - Pendiente: ${(o.MontoTotal - (o.RecepcionMercaderia?.Sum(r => r.CantidadRecibida) ?? 0)):N2}" 
                })
                .ToList();

            ViewBag.OrdenesCompra = new SelectList(ordenesConSaldo, "OrdenCompraID", "Display", ordenCompraId);
            ViewBag.Almacenes = new SelectList(await _context.Almacens
                .ToListAsync(), "AlmacenID", "NombreAlmacen");
            ViewBag.Productos = new SelectList(await _context.Productos
                .Where(p => p.Activo)
                .ToListAsync(), "ProductoID", "NombreProducto");

            return View();
        }

        // POST: Recepciones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecepcionMercaderium recepcion, int productoId, string? lote, DateTime? fechaVencimiento, decimal costoUnitario)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Validar que la orden no esté completada
                    var orden = await _context.OrdenesCompras
                        .Include(o => o.RecepcionMercaderia)
                        .FirstOrDefaultAsync(o => o.OrdenCompraId == recepcion.OrdenCompraId);

                    if (orden == null)
                    {
                        ModelState.AddModelError("", "Orden de compra no encontrada");
                        await LoadDropDowns(recepcion.OrdenCompraId);
                        return View(recepcion);
                    }

                    var totalRecibidoActual = orden.RecepcionMercaderia?.Sum(r => r.CantidadRecibida) ?? 0;
                    var nuevoTotal = totalRecibidoActual + recepcion.CantidadRecibida;

                    if (nuevoTotal > orden.MontoTotal)
                    {
                        ModelState.AddModelError("CantidadRecibida", $"La cantidad excede el monto pendiente. Disponible: {(orden.MontoTotal - totalRecibidoActual):N2}");
                        await LoadDropDowns(recepcion.OrdenCompraId);
                        return View(recepcion);
                    }

                    // Crear inventario
                    var inventario = new Inventario
                    {
                        ProductoId = productoId,
                        Lote = lote,
                        FechaIngreso = DateOnly.FromDateTime(recepcion.FechaRecepcion.ToDateTime(TimeOnly.MinValue)),
                        FechaVencimiento = fechaVencimiento.HasValue ? DateOnly.FromDateTime(fechaVencimiento.Value) : null,
                        CostoUnitario = costoUnitario,
                        PrecioVenta = costoUnitario * 1.3m,
                        FechaRegistro = DateTime.Now
                    };

                    _context.Inventarios.Add(inventario);
                    await _context.SaveChangesAsync();

                    // Asignar inventario a la recepción
                    recepcion.InventarioId = inventario.InventarioId;
                    recepcion.FechaRecepcion = DateOnly.FromDateTime(DateTime.Now);
                    recepcion.UsuarioId = 1; // TODO: Obtener del usuario logueado

                    _context.RecepcionMercaderia.Add(recepcion);
                    await _context.SaveChangesAsync();

                    // Actualizar contenido del almacén
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
                            EstadoContenidoId = 1, // Activo
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error al registrar recepción");
                    ModelState.AddModelError("", "Error al registrar la recepción");
                }
            }

            await LoadDropDowns(recepcion.OrdenCompraId, recepcion.AlmacenId);
            return View(recepcion);
        }

        // POST: Recepciones/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
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
                // Eliminar contenido asociado
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

            return RedirectToAction(nameof(Index));
        }

        // GET: Recepciones/GetProductosPorOrden
        public async Task<IActionResult> GetProductosPorOrden(int ordenCompraId)
        {
            // Por ahora retornamos todos los productos activos
            // En un sistema real, esto podría venir de la orden de compra
            var productos = await _context.Productos
                .Where(p => p.Activo)
                .Select(p => new { p.ProductoId, p.NombreProducto })
                .ToListAsync();

            return Json(productos);
        }

        // POST: Recepciones/ValidarCantidad
        [HttpPost]
        public async Task<IActionResult> ValidarCantidad(int ordenCompraId, decimal cantidad)
        {
            var orden = await _context.OrdenesCompras
                .Include(o => o.RecepcionMercaderia)
                .FirstOrDefaultAsync(o => o.OrdenCompraId == ordenCompraId);

            if (orden == null)
            {
                return Json(new { valido = false, mensaje = "Orden no encontrada" });
            }

            var totalRecibido = orden.RecepcionMercaderia?.Sum(r => r.CantidadRecibida) ?? 0;
            var disponible = orden.MontoTotal - totalRecibido;

            if (cantidad > disponible)
            {
                return Json(new { valido = false, mensaje = $"Cantidad excede el disponible. Disponible: {disponible:N2}" });
            }

            return Json(new { valido = true, disponible = disponible });
        }

        // GET: Recepciones/RecepcionesPorProveedor
        public async Task<IActionResult> RecepcionesPorProveedor(int proveedorId, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var query = _context.RecepcionMercaderia
                .Include(r => r.OrdenCompra)
                .Include(r => r.Inventario)
                    .ThenInclude(i => i != null ? i.Producto : null)
                .Where(r => r.OrdenCompra != null && r.OrdenCompra.ProveedorId == proveedorId);

            if (fechaDesde.HasValue)
            {
                var fechaDesdeDate = DateOnly.FromDateTime(fechaDesde.Value);
                query = query.Where(r => r.FechaRecepcion >= fechaDesdeDate);
            }

            if (fechaHasta.HasValue)
            {
                var fechaHastaDate = DateOnly.FromDateTime(fechaHasta.Value);
                query = query.Where(r => r.FechaRecepcion <= fechaHastaDate);
            }

            var recepciones = await query
                .OrderByDescending(r => r.FechaRecepcion)
                .ToListAsync();

            var proveedor = await _context.Proveedores.FindAsync(proveedorId);
            ViewBag.ProveedorNombre = proveedor?.RazonSocial;

            return View(recepciones);
        }

        private async Task LoadDropDowns(int? ordenCompraId = null, int? almacenId = null)
        {
            var ordenesDisponibles = await _context.OrdenesCompras
                .Include(o => o.Proveedor)
                .Include(o => o.RecepcionMercaderia)
                .ToListAsync();

            var ordenesConSaldo = ordenesDisponibles
                .Where(o => o.RecepcionMercaderia == null || o.RecepcionMercaderia.Sum(r => r.CantidadRecibida) < o.MontoTotal)
                .Select(o => new
                {
                    o.OrdenCompraId,
                    Display = $"{o.NumeroOrden} - {(o.Proveedor != null ? o.Proveedor.RazonSocial : "")} - Pendiente: ${(o.MontoTotal - (o.RecepcionMercaderia?.Sum(r => r.CantidadRecibida) ?? 0)):N2}"
                })
                .ToList();

            ViewBag.OrdenesCompra = new SelectList(ordenesConSaldo, "OrdenCompraID", "Display", ordenCompraId);
            ViewBag.Almacenes = new SelectList(await _context.Almacens
    .ToListAsync(), "AlmacenID", "NombreAlmacen");
            ViewBag.Productos = new SelectList(await _context.Productos
                .Where(p => p.Activo)
                .ToListAsync(), "ProductoID", "NombreProducto");
        }

        private bool RecepcionExists(int id)
        {
            return _context.RecepcionMercaderia.Any(e => e.RecepcionId == id);
        }
    }
}