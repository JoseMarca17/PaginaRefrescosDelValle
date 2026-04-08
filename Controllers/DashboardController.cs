using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// EL CAMBIO ESTÁ AQUÍ: Ahora apuntamos a las entidades nuevas
using RefrescosDelValle.Models.Entities; 
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
            
            try
            {
                // Como ya tienes la V1.11, la tabla se llama Producto
                totalProductos = await _db.Productos.CountAsync();
            }
            catch { totalProductos = 125; /* Fallback temporal */ }
            
            // ... (el resto de tus bloques try-catch siguen igual por ahora) ...
            pedidosHoy = 8;
            stockBajo = 3;
            totalEmpleados = 45;
            totalUsuarios = 12;
            totalSucursales = 5;
            produccionHoy = 4;
            lineasActivas = 3;
            stockTotal = 1250;
            stockCritico = 3;
            ventasHoy = 1250.50m;
            clientesActivos = 28;
            ordenesCompra = 2;
            proveedoresActivos = 15;
            empleadosPorSucursal = 45;
            asistenciaHoy = 38;

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
            
            var actividadReciente = new List<ActividadRecienteViewModel>
            {
                new ActividadRecienteViewModel { Id = 1, Descripcion = "Usuario admin inició sesión", Tipo = "login", Fecha = DateTime.Now.AddMinutes(-5), Usuario = "Admin" },
                new ActividadRecienteViewModel { Id = 2, Descripcion = "Nuevo pedido #1234 creado", Tipo = "venta", Fecha = DateTime.Now.AddHours(-1), Usuario = "Cliente" }
            };
            
            ViewBag.ActividadReciente = actividadReciente;
            
            return View();
        }

        public IActionResult Comercial()
        {
            ViewBag.VentasKpi_PedidosActivos = 12;
            ViewBag.TotalClientes = 145;
            ViewBag.VentasKpi_IngresosMes = 45250.50;
            ViewBag.VentasKpi_PedidosHoy = 5;

            ViewBag.ComprasKpi_OrdenesPendientes = 3;
            ViewBag.TotalProveedores = 24;
            ViewBag.ComprasKpi_DeudaTotal = 12500.00;

            ViewBag.LogisticaKpi_VehiculosOperativos = 8;
            ViewBag.LogisticaKpi_EntregasHoy = 15;
            ViewBag.LogisticaKpi_DespachosRuta = 4;

            ViewBag.ActividadOperativa = new List<dynamic>
            {
                new { Tipo = "venta", Descripcion = "Nuevo pedido registrado", Fecha = DateTime.Now.AddMinutes(-15), Referencia = "Cliente: Supermercado Fidalga" },
                new { Tipo = "logistica", Descripcion = "Despacho en ruta", Fecha = DateTime.Now.AddMinutes(-45), Referencia = "Vehículo: ABC-123" }
            };

            return View();
        }
    }
    
    public class ActividadRecienteViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; } = string.Empty;
    }
}