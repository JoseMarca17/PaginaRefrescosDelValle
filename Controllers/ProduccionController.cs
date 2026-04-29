using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Controllers
{
    public class ProduccionController : Controller
    {
        private readonly AppDbContext _context;

        public ProduccionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Produccion/Index
        public IActionResult Index()
        {
            ViewBag.TotalOrdenes = 156;
            ViewBag.OrdenesEnCurso = 8;
            ViewBag.EficienciaGlobal = 87.5;
            ViewBag.ProduccionHoy = 12450;

            return View();
        }

        // GET: /Produccion/GetOrdenes
        [HttpGet]
        public IActionResult GetOrdenes()
        {
            var ordenes = new List<object>
            {
                new
                {
                    Id = 1,
                    NumeroOrden = "#ORD-2401-001",
                    ProductoNombre = "Refresco Cola",
                    CantidadTotal = 5000,
                    CantidadProducida = 3250,
                    Estado = "En Proceso",
                    LineaNombre = "Línea 1",
                    Progreso = 65,
                    FechaInicio = "15/01/2024"
                },
                new
                {
                    Id = 2,
                    NumeroOrden = "#ORD-2401-002",
                    ProductoNombre = "Naranja Sabor Intenso",
                    CantidadTotal = 3200,
                    CantidadProducida = 0,
                    Estado = "Planificada",
                    LineaNombre = "Línea 2",
                    Progreso = 0,
                    FechaInicio = "22/01/2024"
                },
                new
                {
                    Id = 3,
                    NumeroOrden = "#ORD-2401-003",
                    ProductoNombre = "Lima Limón Zero",
                    CantidadTotal = 4500,
                    CantidadProducida = 1890,
                    Estado = "Pausada",
                    LineaNombre = "Línea 3",
                    Progreso = 42,
                    FechaInicio = "10/01/2024"
                }
            };

            return Json(new
            {
                success = true,
                data = ordenes
            });
        }

        // POST: /Produccion/CrearOrden
        [HttpPost]
        public IActionResult CrearOrden([FromBody] dynamic model)
        {
            return Json(new
            {
                success = true,
                message = "Orden creada exitosamente"
            });
        }

        // POST: /Produccion/ActualizarEstado
        [HttpPost]
        public IActionResult ActualizarEstado(int id, string estado)
        {
            return Json(new
            {
                success = true,
                message = "Estado actualizado"
            });
        }

        /*
        =========================================
        PRODUCTOS - CRUD REAL
        =========================================
        */

        // GET: Produccion/Productos
        public async Task<IActionResult> Productos(string? searchString, bool? activo)
        {
            var query = _context.Productos
                .Include(p => p.CategoriaProducto)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p =>
                    p.NombreProducto.Contains(searchString) ||
                    (p.CodigoSku != null && p.CodigoSku.Contains(searchString)));
            }

            if (activo.HasValue)
            {
                query = query.Where(p => p.Activo == activo.Value);
            }

            var productos = await query
                .OrderBy(p => p.NombreProducto)
                .ToListAsync();

            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentActivo = activo;

            return View(productos);
        }

        // GET: Produccion/ProductoDetails/5
        public async Task<IActionResult> ProductoDetails(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.CategoriaProducto)
                .Include(p => p.OrdenProduccions)
                .FirstOrDefaultAsync(p => p.ProductoId == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Produccion/CrearProducto
        public async Task<IActionResult> CrearProducto()
        {
            ViewBag.Categorias = new SelectList(
                await _context.CategoriaProductos
                    .Where(c => c.Activo)
                    .OrderBy(c => c.Nombre)
                    .ToListAsync(),
                "CategoriaProductoId",
                "Nombre"
            );

            return View();
        }

        // POST: Produccion/CrearProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearProducto(Producto producto)
        {
            if (!string.IsNullOrEmpty(producto.CodigoSku) &&
                await _context.Productos.AnyAsync(p => p.CodigoSku == producto.CodigoSku))
            {
                ModelState.AddModelError(
                    "CodigoSku",
                    "El código SKU ya está registrado"
                );
            }

            if (ModelState.IsValid)
            {
                try
                {
                    producto.FechaCreacion = DateTime.Now;
                    producto.Activo = true;

                    _context.Productos.Add(producto);
                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Producto creado exitosamente";

                    return RedirectToAction(nameof(Productos));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(
                        "",
                        "Error al guardar el producto"
                    );
                }
            }

            ViewBag.Categorias = new SelectList(
                await _context.CategoriaProductos
                    .Where(c => c.Activo)
                    .ToListAsync(),
                "CategoriaProductoId",
                "Nombre",
                producto.CategoriaProductoId
            );

            return View(producto);
        }

        // =========================================
        // GET: Produccion/EditarProducto/5
        // =========================================
        public async Task<IActionResult> EditarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = new SelectList(
                await _context.CategoriaProductos
                    .Where(c => c.Activo)
                    .OrderBy(c => c.Nombre)
                    .ToListAsync(),
                "CategoriaProductoId",
                "Nombre",
                producto.CategoriaProductoId
            );

            return View(producto);
        }

        // =========================================
        // POST: Produccion/EditarProducto/5
        // =========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarProducto(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(producto.CodigoSku))
            {
                var existeSku = await _context.Productos.AnyAsync(p =>
                    p.CodigoSku == producto.CodigoSku &&
                    p.ProductoId != producto.ProductoId);

                if (existeSku)
                {
                    ModelState.AddModelError(
                        "CodigoSku",
                        "El código SKU ya está registrado"
                    );
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productoExistente = await _context.Productos
                        .FirstOrDefaultAsync(p => p.ProductoId == id);

                    if (productoExistente == null)
                    {
                        return NotFound();
                    }

                    productoExistente.NombreProducto = producto.NombreProducto;
                    productoExistente.CodigoSku = producto.CodigoSku;
                    productoExistente.CategoriaProductoId = producto.CategoriaProductoId;
                    productoExistente.UnidadMedidaTexto = producto.UnidadMedidaTexto;
                    productoExistente.Descripcion = producto.Descripcion;
                    productoExistente.Activo = producto.Activo;

                    await _context.SaveChangesAsync();

                    TempData["Success"] = "Producto actualizado exitosamente";

                    return RedirectToAction(nameof(Productos));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(
                        "",
                        "Ocurrió un error al actualizar el producto"
                    );
                }
            }

            ViewBag.Categorias = new SelectList(
                await _context.CategoriaProductos
                    .Where(c => c.Activo)
                    .OrderBy(c => c.Nombre)
                    .ToListAsync(),
                "CategoriaProductoId",
                "Nombre",
                producto.CategoriaProductoId
            );

            return View(producto);
        }

        // =========================================
        // POST: Produccion/EliminarProducto/5
        // =========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.OrdenProduccions)
                .Include(p => p.Inventarios)
                .Include(p => p.Contenidos)
                .FirstOrDefaultAsync(p => p.ProductoId == id);

            if (producto == null)
            {
                return NotFound();
            }

            // Validación importante: evitar eliminar si tiene relaciones
            if ((producto.OrdenProduccions != null && producto.OrdenProduccions.Any()) ||
                (producto.Inventarios != null && producto.Inventarios.Any()) ||
                (producto.Contenidos != null && producto.Contenidos.Any()))
            {
                TempData["Error"] = "No se puede eliminar el producto porque tiene registros relacionados.";

                return RedirectToAction(nameof(Productos));
            }

            try
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Producto eliminado correctamente.";
            }
            catch (Exception)
            {
                TempData["Error"] = "Ocurrió un error al eliminar el producto.";
            }

            return RedirectToAction(nameof(Productos));
        }
    }
}