using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities;

namespace RefrescosDelValle.Controllers
{
    public class RecetasController : Controller
    {
        private readonly AppDbContext _context;

        public RecetasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Recetas/Index
        public IActionResult Index()
        {
            ViewBag.TotalRecetas = 24;
            ViewBag.RecetasActivas = 18;
            ViewBag.RecetasConfidenciales = 8;
            ViewBag.VersionesPendientes = 5;
            
            return View();
        }

        // GET: /Recetas/GetRecetas
        [HttpGet]
        public IActionResult GetRecetas()
        {
            var recetas = new List<object>
            {
                new { Id = 1, ProductoNombre = "Refresco Cola", Version = "2.1.0", EstadoRecetaID = 1, EsConfidencial = true, IngredientesCount = 8 },
                new { Id = 2, ProductoNombre = "Naranja Sabor Intenso", Version = "1.5.2", EstadoRecetaID = 1, EsConfidencial = true, IngredientesCount = 6 },
                new { Id = 3, ProductoNombre = "Lima Limón Zero", Version = "3.0.0", EstadoRecetaID = 1, EsConfidencial = false, IngredientesCount = 7 },
                new { Id = 4, ProductoNombre = "Manzana Verde", Version = "1.2.0", EstadoRecetaID = 2, EsConfidencial = false, IngredientesCount = 5 }
            };
            
            return Json(new { success = true, data = recetas });
        }

        // GET: /Recetas/ObtenerReceta
        [HttpGet]
        public IActionResult ObtenerReceta(int id)
        {
            var receta = new
            {
                RecetaID = id,
                ProductoID = 1,
                ProductoNombre = "Refresco Cola",
                Version = "2.1.0",
                TipoRecetaID = 1,
                EstadoRecetaID = 1,
                EsConfidencial = true,
                Detalles = new List<object>
                {
                    new { IngredienteID = 1, IngredienteNombre = "Jarabe de Maíz", Cantidad = 25.5, UnidadMedidaID = 1, UnidadMedidaNombre = "kg", OrdenProceso = 1 },
                    new { IngredienteID = 3, IngredienteNombre = "Agua Purificada", Cantidad = 100, UnidadMedidaID = 2, UnidadMedidaNombre = "L", OrdenProceso = 2 }
                }
            };
            
            return Json(new { success = true, data = receta });
        }

        // POST: /Recetas/CrearReceta
        [HttpPost]
        public IActionResult CrearReceta([FromBody] dynamic model)
        {
            return Json(new { success = true, message = "Receta creada exitosamente" });
        }

        // POST: /Recetas/GuardarDetalle
        [HttpPost]
        public IActionResult GuardarDetalle([FromBody] List<dynamic> detalles)
        {
            return Json(new { success = true, message = $"Se guardaron {detalles.Count} ingredientes" });
        }
    }
}