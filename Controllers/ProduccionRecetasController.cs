```csharp
// Controllers/ProduccionRecetasController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace RefrescosDelValle.Controllers
{
    public class ProduccionRecetasController : Controller
    {
        // GET: /ProduccionRecetas/Index
        public IActionResult Index()
        {
            // Datos de ejemplo para la vista
            ViewBag.TotalRecetasActivas = 24;
            ViewBag.OrdenesEnCurso = 8;
            ViewBag.EficienciaProduccion = 87.5;
            
            return View();
        }

        // POST: /ProduccionRecetas/CrearReceta
        [HttpPost]
        public IActionResult CrearReceta([FromBody] RecetaViewModel receta)
        {
            if (ModelState.IsValid)
            {
                // Aquí iría la lógica para guardar en BD
                return Json(new { success = true, message = "Receta creada exitosamente" });
            }
            return Json(new { success = false, message = "Error al crear receta" });
        }

        // POST: /ProduccionRecetas/GuardarDetalle
        [HttpPost]
        public IActionResult GuardarDetalle([FromBody] List<DetalleRecetaViewModel> detalles)
        {
            if (detalles != null && detalles.Any())
            {
                // Aquí iría la lógica para guardar detalles en BD
                return Json(new { success = true, message = $"Se guardaron {detalles.Count} ingredientes" });
            }
            return Json(new { success = false, message = "No hay ingredientes para guardar" });
        }

        // GET: /ProduccionRecetas/ObtenerReceta/{id}
        [HttpGet]
        public IActionResult ObtenerReceta(int id)
        {
            // Datos de ejemplo
            var receta = new RecetaViewModel
            {
                Id = id,
                NombreProducto = "Refresco Cola",
                Version = "2.1.0",
                TipoRecetaID = 1,
                EstadoRecetaID = 1,
                EsConfidencial = true
            };
            
            return Json(receta);
        }
    }

    // ViewModels
    public class RecetaViewModel
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; }
        public string Version { get; set; }
        public int TipoRecetaID { get; set; }
        public int EstadoRecetaID { get; set; }
        public bool EsConfidencial { get; set; }
    }

    public class DetalleRecetaViewModel
    {
        public int IngredienteID { get; set; }
        public decimal Cantidad { get; set; }
        public int UnidadMedidaID { get; set; }
        public int OrdenProceso { get; set; }
    }
}
```