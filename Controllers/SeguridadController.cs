using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RefrescosDelValle.Models.ViewModels;

namespace RefrescosDelValle.Controllers
{
    // Restringimos esta zona solo a los administradores de la red
    [Authorize(Policy = "Seguridad")] 
    public class SeguridadController : Controller
    {
        // GET /Seguridad/RegistrarPersonal
        public IActionResult RegistrarPersonal()
        {
            // === DATOS SIMULADOS (MOCKS) PARA LA INTERFAZ ===
            // Como aún no tienes datos en la BD, inventamos algunos para que la UI no reviente.

            ViewBag.Sucursales = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "NODO 01 - Matriz Central (La Paz)" },
                new SelectListItem { Value = "2", Text = "NODO 02 - Planta Producción (El Alto)" },
                new SelectListItem { Value = "3", Text = "NODO 03 - Distribuidora (Cochabamba)" }
            };

            ViewBag.Cargos = new List<SelectListItem>
            {
                new SelectListItem { Value = "10", Text = "Gerente de Operaciones" },
                new SelectListItem { Value = "11", Text = "Supervisor de Planta" },
                new SelectListItem { Value = "12", Text = "Auditor de Seguridad (SysAdmin)" },
                new SelectListItem { Value = "13", Text = "Ejecutivo de Ventas" }
            };

            // Estos son los roles reales que configuraste en tu Program.cs
            ViewBag.RolesDisponibles = new List<SelectListItem>
            {
                new SelectListItem { Value = "SuperAdmin", Text = "SuperAdmin (Acceso Total)" },
                new SelectListItem { Value = "AdminSeguridad", Text = "Admin. de Seguridad" },
                new SelectListItem { Value = "AdminProduccion", Text = "Admin. de Producción" },
                new SelectListItem { Value = "AdminInventario", Text = "Admin. de Inventarios" },
                new SelectListItem { Value = "AdminVentas", Text = "Admin. de Ventas" },
                new SelectListItem { Value = "AdminCompras", Text = "Admin. de Compras" },
                new SelectListItem { Value = "AdminRRHH", Text = "Admin. de RRHH" }
            };

            return View(new RegistroEmpleadoViewModel());
        }

        // POST /Seguridad/RegistrarPersonal
        [HttpPost]
        public IActionResult RegistrarPersonal(RegistroEmpleadoViewModel model)
        {
            // Aquí iría el código real con la transacción a la base de datos.
            // Por ahora, solo simularemos que todo salió perfecto para tu demo.
            
            TempData["SuccessMsg"] = $"Operación exitosa: El usuario {model.Email} ha sido dado de alta en la red con {model.RolesSeleccionados.Count} privilegios asignados.";
            
            // Recargamos la misma vista para seguir registrando gente (como es un mockup)
            return RedirectToAction("RegistrarPersonal");
        }
    }
}