using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                new { Id = 1, NumeroOrden = "#ORD-2401-001", ProductoNombre = "Refresco Cola", CantidadTotal = 5000, CantidadProducida = 3250, Estado = "En Proceso", LineaNombre = "Línea 1", Progreso = 65, FechaInicio = "15/01/2024" },
                new { Id = 2, NumeroOrden = "#ORD-2401-002", ProductoNombre = "Naranja Sabor Intenso", CantidadTotal = 3200, CantidadProducida = 0, Estado = "Planificada", LineaNombre = "Línea 2", Progreso = 0, FechaInicio = "22/01/2024" },
                new { Id = 3, NumeroOrden = "#ORD-2401-003", ProductoNombre = "Lima Limón Zero", CantidadTotal = 4500, CantidadProducida = 1890, Estado = "Pausada", LineaNombre = "Línea 3", Progreso = 42, FechaInicio = "10/01/2024" }
            };
            
            return Json(new { success = true, data = ordenes });
        }

        // POST: /Produccion/CrearOrden
        [HttpPost]
        public IActionResult CrearOrden([FromBody] dynamic model)
        {
            return Json(new { success = true, message = "Orden creada exitosamente" });
        }

        // POST: /Produccion/ActualizarEstado
        [HttpPost]
        public IActionResult ActualizarEstado(int id, string estado)
        {
            return Json(new { success = true, message = "Estado actualizado" });
        }
    }
}