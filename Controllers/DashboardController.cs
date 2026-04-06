using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Data;
using System.Security.Claims;

namespace RefrescosDelValle.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int userId = 0;
            if (!string.IsNullOrEmpty(userIdClaim))
            {
                int.TryParse(userIdClaim, out userId);
            }
            int totalProductos = 0;
            int pedidosHoy = 0;
            int stockBajo = 0;
            int totalEmpleados = 0;
            int totalUsuarios = 0;
            int totalSucursales = 0;
            int produccionHoy = 0;
            int lineasActivas = 0;
            int stockTotal = 0;
            int stockCritico = 0;
            decimal ventasHoy = 0;
            int clientesActivos = 0;
            int ordenesCompra = 0;
            int proveedoresActivos = 0;
            int empleadosPorSucursal = 0;
            int asistenciaHoy = 0;

            // ==================== OBTENER DATOS REALES ====================
            // Reemplaza con los nombres reales de tus entidades
            
            // Productos - AJUSTA EL NOMBRE DE TU ENTIDAD
            try
            {
                // Si tu entidad se llama "Productos"
                // totalProductos = await _db.Productos.CountAsync();
                
                // Si se llama "InventarioProductos"
                // totalProductos = await _db.InventarioProductos.CountAsync();
                
                // Temporal mientras defines los nombres
                totalProductos = 125; // Valor de ejemplo
            }
            catch (Exception ex)
            {
                // Log del error
                totalProductos = 0;
            }
            
            // Pedidos - AJUSTA EL NOMBRE DE TU ENTIDAD
            try
            {
                // pedidosHoy = await _db.Pedidos.CountAsync(p => p.Fecha.Date == DateTime.Today);
                pedidosHoy = 8; // Valor de ejemplo
            }
            catch { pedidosHoy = 0; }
            
            // Stock Bajo - AJUSTA SEGÚN TU ESTRUCTURA
            try
            {
                // stockBajo = await _db.Productos.CountAsync(p => p.StockActual <= p.StockMinimo);
                stockBajo = 3; // Valor de ejemplo
            }
            catch { stockBajo = 0; }
            
            // Empleados
            try
            {
                // totalEmpleados = await _db.Empleados.CountAsync();
                totalEmpleados = 45; // Valor de ejemplo
            }
            catch { totalEmpleados = 0; }
            
            // Usuarios
            try
            {
                // totalUsuarios = await _db.Usuarios.CountAsync();
                totalUsuarios = 12; // Valor de ejemplo
            }
            catch { totalUsuarios = 0; }
            
            // Sucursales
            try
            {
                // totalSucursales = await _db.Sucursales.CountAsync();
                totalSucursales = 5; // Valor de ejemplo
            }
            catch { totalSucursales = 0; }
            
            // Producción
            try
            {
                // produccionHoy = await _db.OrdenesProduccion.CountAsync();
                produccionHoy = 4; // Valor de ejemplo
                // lineasActivas = await _db.LineasProduccion.CountAsync();
                lineasActivas = 3; // Valor de ejemplo
            }
            catch { produccionHoy = 0; lineasActivas = 0; }
            
            // Stock
            try
            {
                // stockTotal = await _db.Productos.SumAsync(p => p.StockActual);
                stockTotal = 1250; // Valor de ejemplo
                // stockCritico = await _db.Productos.CountAsync(p => p.StockActual <= p.StockMinimo && p.StockActual > 0);
                stockCritico = 3; // Valor de ejemplo
            }
            catch { stockTotal = 0; stockCritico = 0; }
            
            // Ventas
            try
            {
                // ventasHoy = await _db.Pedidos.Where(p => p.Fecha.Date == DateTime.Today).SumAsync(p => p.Total);
                ventasHoy = 1250.50m; // Valor de ejemplo
                // clientesActivos = await _db.Clientes.CountAsync();
                clientesActivos = 28; // Valor de ejemplo
            }
            catch { ventasHoy = 0; clientesActivos = 0; }
            
            // Compras
            try
            {
                // ordenesCompra = await _db.OrdenesCompra.CountAsync();
                ordenesCompra = 2; // Valor de ejemplo
                // proveedoresActivos = await _db.Proveedores.CountAsync();
                proveedoresActivos = 15; // Valor de ejemplo
            }
            catch { ordenesCompra = 0; proveedoresActivos = 0; }
            
            // RRHH
            try
            {
                // empleadosPorSucursal = await _db.Empleados.CountAsync();
                empleadosPorSucursal = 45; // Valor de ejemplo
                // asistenciaHoy = await _db.Asistencias.CountAsync(a => a.Fecha.Date == DateTime.Today);
                asistenciaHoy = 38; // Valor de ejemplo
            }
            catch { empleadosPorSucursal = 0; asistenciaHoy = 0; }

            // ==================== ASIGNAR VALORES AL ViewBag ====================
            ViewBag.TotalProductos = totalProductos;
            ViewBag.PedidosHoy = pedidosHoy;
            ViewBag.StockBajo = stockBajo;
            ViewBag.TotalEmpleados = totalEmpleados;
            ViewBag.TotalUsuarios = totalUsuarios;
            ViewBag.TotalSucursales = totalSucursales;
            ViewBag.ProduccionHoy = produccionHoy;
            ViewBag.LineasActivas = lineasActivas;
            ViewBag.StockTotal = stockTotal;
            ViewBag.StockCritico = stockCritico;
            ViewBag.VentasHoy = ventasHoy;
            ViewBag.VentasHoyMonto = ventasHoy.ToString("N2");
            ViewBag.ClientesActivos = clientesActivos;
            ViewBag.OrdenesCompra = ordenesCompra;
            ViewBag.ProveedoresActivos = proveedoresActivos;
            ViewBag.EmpleadosPorSucursal = empleadosPorSucursal;
            ViewBag.AsistenciaHoy = asistenciaHoy;
            
            // Actividad reciente (si tienes tabla de bitácora)
            var actividadReciente = new List<ActividadRecienteViewModel>();
            
            // Si tienes tabla de bitácora, descomenta y ajusta:
            /*
            try
            {
                actividadReciente = await _db.Bitacora
                    .OrderByDescending(b => b.Fecha)
                    .Take(10)
                    .Select(b => new ActividadRecienteViewModel
                    {
                        Id = b.Id,
                        Descripcion = b.Accion,
                        Tipo = b.Tipo,
                        Fecha = b.Fecha,
                        Usuario = b.Usuario
                    })
                    .ToListAsync();
            }
            catch { }
            */
            
            // Datos de ejemplo para actividad reciente
            if (actividadReciente.Count == 0)
            {
                actividadReciente = new List<ActividadRecienteViewModel>
                {
                    new ActividadRecienteViewModel { Id = 1, Descripcion = "Usuario admin inició sesión", Tipo = "login", Fecha = DateTime.Now.AddMinutes(-5), Usuario = "Admin" },
                    new ActividadRecienteViewModel { Id = 2, Descripcion = "Nuevo pedido #1234 creado", Tipo = "venta", Fecha = DateTime.Now.AddHours(-1), Usuario = "Cliente" },
                    new ActividadRecienteViewModel { Id = 3, Descripcion = "Producto actualizado: Refresco Cola", Tipo = "produccion", Fecha = DateTime.Now.AddHours(-2), Usuario = "Producción" },
                    new ActividadRecienteViewModel { Id = 4, Descripcion = "Stock crítico: Refresco Naranja", Tipo = "inventario", Fecha = DateTime.Now.AddHours(-3), Usuario = "Sistema" },
                    new ActividadRecienteViewModel { Id = 5, Descripcion = "Nuevo proveedor registrado", Tipo = "compras", Fecha = DateTime.Now.AddHours(-4), Usuario = "Compras" }
                };
            }
            
            ViewBag.ActividadReciente = actividadReciente;
            
            return View();
        }
    }
    
    // ViewModel para actividad reciente
    public class ActividadRecienteViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; } = string.Empty;
    }
}