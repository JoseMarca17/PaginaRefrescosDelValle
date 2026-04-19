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

        public async Task<IActionResult> IndexInventario()
        {
            var vm = new InventarioIndexViewModel
            {
                TotalUnidades = (int)await _context.Contenidos.SumAsync(c => c.CantidadDisponible),
                TotalAlmacenes = await _context.Almacens.CountAsync(),
                TotalDepartamentos = await _context.Almacens
                    .Include(a => a.Sucursal)
                        .ThenInclude(s => s.Ciudad)
                    .Select(a => a.Sucursal.Ciudad.DepartamentoGeoId)
                    .Distinct()
                    .CountAsync()
            };

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

        public async Task<IActionResult> Almacenes()
        {
            var almacenes = await _context.Almacens
                .Include(a => a.Sucursal)
                    .ThenInclude(s => s.Ciudad)
                        .ThenInclude(c => c.DepartamentoGeo)  // ✅ DepartamentoGeo, no Departamento
                .Include(a => a.TipoAlmacen)
                .Include(a => a.EstadoAlmacen)
                .ToListAsync();

            return View(almacenes);
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