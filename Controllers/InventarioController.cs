using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities; // Para acceder al AppDbContext y la Vista
using RefrescosDelValle.Models.ViewModels;
using System.Threading.Tasks;

namespace RefrescosDelValle.Controllers
{
    //[Authorize]
    public class InventarioController : Controller
    {
        private readonly AppDbContext _context;

        // 1. INYECCIÓN DEL CONTEXTO DE BASE DE DATOS
        public InventarioController(AppDbContext context)
        {
            _context = context;
        }

        // Reemplaza tu método IndexInventario() en InventarioController.cs por este completo:

        public async Task<IActionResult> IndexInventario()
        {
            var ahora = DateTime.Now;
            var inicioMes = new DateTime(ahora.Year, ahora.Month, 1);
            var inicioMesDate = new DateOnly(ahora.Year, ahora.Month, 1);

            var vm = new InventarioIndexViewModel
            {
                // ── Hero ──
                TotalUnidades = (int)await _context.Contenidos
                    .SumAsync(c => c.CantidadDisponible),

                TotalAlmacenes = await _context.Almacens
                    .CountAsync(),

                TotalDepartamentos = await _context.Almacens
                    .Include(a => a.Sucursal)
                        .ThenInclude(s => s.Ciudad)
                    .Select(a => a.Sucursal.Ciudad.DepartamentoGeoId)
                    .Distinct()
                    .CountAsync(),

                // ── Card Stock ──
                TotalSKUs = await _context.Contenidos
                    .Select(c => c.ProductoId)
                    .Distinct()
                    .CountAsync(),

                // Productos cuyo stock total disponible es <= 50 unidades (ajusta el umbral)
                SKUsCriticos = await _context.Contenidos
                    .GroupBy(c => c.ProductoId)
                    .CountAsync(g => g.Sum(c => c.CantidadDisponible) <= 50),

                // ── Card Almacenes ──
                AlmacenesActivos = await _context.Almacens
                    .Include(a => a.EstadoAlmacen)
                    .CountAsync(a => a.EstadoAlmacen.Descripcion == "Activo"),

                AlmacenesEnBaja = await _context.Almacens
                    .Include(a => a.EstadoAlmacen)
                    .CountAsync(a => a.EstadoAlmacen.Descripcion == "Baja"),

                // ── Card Movimientos ──
                TrasladosEsteMes = await _context.Movimientos
                    .Include(m => m.MovimientoDetalle)
                    .CountAsync(m => m.MovimientoDetalle.FechaEnvio >= inicioMesDate),

                MermasEsteMes = await _context.VwMermas
                    .CountAsync(v => v.FechaMerma >= inicioMesDate),
            };

            // Total = traslados + mermas del mes
            vm.MovimientosEsteMes = vm.TrasladosEsteMes + vm.MermasEsteMes;

            return View(vm);
        }


        // 2. ACCIÓN DE STOCK (El corazón del PASO 2)
        public async Task<IActionResult> Stock()
        {
            // Consultamos la vista de SQL directamente
            var listaStock = await _context.VwStockAlmacens.ToListAsync();

            // Enviamos los datos a la página web (.cshtml)
            return View(listaStock);
        }

        //public async Task<IActionResult> Almacenes()
        //{
        //    var almacenes = await _context.Almacens
        //        .Include(a => a.Sucursal)
        //            .ThenInclude(s => s.Ciudad)
        //                .ThenInclude(c => c.DepartamentoGeo)  // ✅ DepartamentoGeo, no Departamento
        //        .Include(a => a.TipoAlmacen)
        //        .Include(a => a.EstadoAlmacen)
        //        .ToListAsync();

        //    return View(almacenes);
        //}
        // ── ALMACENES (vista principal) ────────────────────────────────────
        // Reemplaza tu método Almacenes() existente por este:
        public async Task<IActionResult> Almacenes()
        {
            var almacenes = await _context.Almacens
                .Include(a => a.Sucursal)
                    .ThenInclude(s => s.Ciudad)
                        .ThenInclude(c => c.DepartamentoGeo)
                .Include(a => a.TipoAlmacen)
                .Include(a => a.EstadoAlmacen)
                .OrderBy(a => a.NombreAlmacen)
                .ToListAsync();

            // Dropdowns para el panel lateral
            await CargarViewBagAlmacen();

            return View(almacenes);
        }

        // ── CREAR ──────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearAlmacen(
            string NombreAlmacen,
            int TipoAlmacenId,
            int SucursalId,
            int EstadoAlmacenId,
            string? Direccion,
            string? Observaciones)
        {
            if (string.IsNullOrWhiteSpace(NombreAlmacen) || TipoAlmacenId == 0 || SucursalId == 0)
            {
                TempData["Error"] = "Faltan datos requeridos para crear el almacén.";
                return RedirectToAction(nameof(Almacenes));
            }

            var almacen = new Almacen
            {
                NombreAlmacen = NombreAlmacen.Trim(),
                TipoAlmacenId = TipoAlmacenId,
                SucursalId = SucursalId,
                EstadoAlmacenId = EstadoAlmacenId,
                Direccion = string.IsNullOrWhiteSpace(Direccion) ? null : Direccion.Trim(),
                Observaciones = string.IsNullOrWhiteSpace(Observaciones) ? null : Observaciones.Trim(),
                FechaCreacion = DateTime.Now
            };

            _context.Almacens.Add(almacen);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Almacén \"{almacen.NombreAlmacen}\" creado correctamente.";
            return RedirectToAction(nameof(Almacenes));
        }

        // ── EDITAR ─────────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarAlmacen(
            int AlmacenId,
            string NombreAlmacen,
            int TipoAlmacenId,
            int SucursalId,
            int EstadoAlmacenId,
            string? Direccion,
            string? Observaciones)
        {
            var almacen = await _context.Almacens.FindAsync(AlmacenId);
            if (almacen == null)
            {
                TempData["Error"] = "Almacén no encontrado.";
                return RedirectToAction(nameof(Almacenes));
            }

            if (string.IsNullOrWhiteSpace(NombreAlmacen) || TipoAlmacenId == 0 || SucursalId == 0)
            {
                TempData["Error"] = "Faltan datos requeridos para editar el almacén.";
                return RedirectToAction(nameof(Almacenes));
            }

            almacen.NombreAlmacen = NombreAlmacen.Trim();
            almacen.TipoAlmacenId = TipoAlmacenId;
            almacen.SucursalId = SucursalId;
            almacen.EstadoAlmacenId = EstadoAlmacenId;
            almacen.Direccion = string.IsNullOrWhiteSpace(Direccion) ? null : Direccion.Trim();
            almacen.Observaciones = string.IsNullOrWhiteSpace(Observaciones) ? null : Observaciones.Trim();
            almacen.FechaModificacion = DateTime.Now;

            _context.Almacens.Update(almacen);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Almacén \"{almacen.NombreAlmacen}\" actualizado correctamente.";
            return RedirectToAction(nameof(Almacenes));
        }

        // ── ELIMINAR ───────────────────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarAlmacen(int id)
        {
            var almacen = await _context.Almacens
                .Include(a => a.Contenidos)
                .FirstOrDefaultAsync(a => a.AlmacenId == id);

            if (almacen == null)
            {
                TempData["Error"] = "Almacén no encontrado.";
                return RedirectToAction(nameof(Almacenes));
            }

            // Evitar eliminar si tiene contenidos asociados
            if (almacen.Contenidos.Any())
            {
                TempData["Error"] = $"No se puede eliminar \"{almacen.NombreAlmacen}\" porque tiene contenidos registrados.";
                return RedirectToAction(nameof(Almacenes));
            }

            _context.Almacens.Remove(almacen);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Almacén \"{almacen.NombreAlmacen}\" eliminado correctamente.";
            return RedirectToAction(nameof(Almacenes));
        }

        // ── HELPER PRIVADO ─────────────────────────────────────────────────
        // Carga los dropdowns que necesita el panel lateral.
        // Ajusta los DominioTipoId según los valores reales en tu BD.
        private async Task CargarViewBagAlmacen()
        {
            // TiposAlmacen: filtra DominioValor por el grupo de tipos de almacén.
            // Cambia el DominioTipoId por el que corresponde en tu tabla DominioValor.
            ViewBag.TiposAlmacen = await _context.DominioValors
                .Where(d => d.DominioTipoId == 20)   // ← ajusta este ID
                .OrderBy(d => d.Descripcion)
                .ToListAsync();

            // EstadosAlmacen: igual, filtra por el grupo de estados.
            ViewBag.EstadosAlmacen = await _context.DominioValors
                .Where(d => d.DominioTipoId == 21)   // ← ajusta este ID
                .OrderBy(d => d.Descripcion)
                .ToListAsync();

            // Sucursales
            ViewBag.Sucursales = await _context.Sucursales
                .OrderBy(s => s.NombreSucursal)
                .ToListAsync();
        }

        public async Task<IActionResult> Movimientos()
        {
            // Traslados desde Movimiento → MovimientoDetalle
            var traslados = await _context.Movimientos
                .Include(m => m.MovimientoDetalle)
                .Include(m => m.TipoMovimiento)
                .Include(m => m.MedioTransporte)  // ✅ agregado
                .Select(m => new MovimientoViewModel
                {
                    Id = "MOV-" + m.MovimientoId,
                    FechaEnvio = m.MovimientoDetalle.FechaEnvio.ToString("yyyy-MM-dd"),
                    FechaRecepcion = m.MovimientoDetalle.FechaRecepcion.HasValue
                                     ? m.MovimientoDetalle.FechaRecepcion.Value.ToString("yyyy-MM-dd")
                                     : null,
                    Origen = m.MovimientoDetalle.Origen,
                    Destino = m.MovimientoDetalle.Destino,
                    EsMerma = false,
                    Cantidad = m.MovimientoDetalle.Cantidad,
                    Descripcion = m.MovimientoDetalle.Descripcion,
                    Tipo = m.TipoMovimiento.Descripcion,
                    Transporte = m.MedioTransporte != null   // ✅ agregado
                                     ? m.MedioTransporte.TipoVehiculo + " · " + m.MedioTransporte.Placa
                                     : null
                })
                .ToListAsync();

            // Mermas desde VwMerma (ya tiene todo desnormalizado)
            var mermas = await _context.VwMermas
                .Select(v => new MovimientoViewModel
                {
                    Id = "MRM-" + v.MermaId,
                    FechaEnvio = v.FechaMerma.ToString("yyyy-MM-dd"),
                    FechaRecepcion = null,
                    Origen = v.NombreAlmacen + " · " + v.NombreSucursal,
                    Destino = "—",
                    EsMerma = true,
                    Cantidad = v.CantidadPerdida,
                    Descripcion = v.Causa ?? v.Observaciones,
                    Tipo = v.TipoMerma
                })
                .ToListAsync();

            var todos = traslados.Concat(mermas)
                .OrderByDescending(m => m.FechaEnvio)
                .ToList();

            var vm = new MovimientosPageViewModel
            {
                Movimientos = todos,
                TotalMovimientos = todos.Count,
                TotalTraslados = traslados.Count,
                TotalMermas = mermas.Count,
                TotalUnidadesMovidas = traslados.Sum(m => m.Cantidad)
            };

            return View(vm);
        }
    }
}
