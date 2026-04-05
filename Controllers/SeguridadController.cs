using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RefrescosDelValle.Models.ViewModels;

namespace RefrescosDelValle.Controllers
{
    [Authorize(Policy = "Seguridad")] 
    public class SeguridadController : Controller
    {
        // GET: /Seguridad/Index
        public IActionResult Index() => View();

        // GET: /Seguridad/Roles
        public IActionResult Roles() => View();

        // GET: /Seguridad/Auditoria
        public IActionResult Auditoria() => View();

        // GET: /Seguridad/SesionesActivas
        public IActionResult SesionesActivas() => View();

        // GET: /Seguridad/RegistrarPersonal

        // GET: /Seguridad/Sucursales
        public IActionResult Sucursales() => View();

        // GET: /Seguridad/Usuarios
        public IActionResult Usuarios() => View();

        // GET: /Seguridad/Sesiones
        public IActionResult Sesiones() => View();
        public IActionResult RegistrarPersonal()
        {
            // Mocks existentes...
            PrepararViewBags();
            return View(new RegistroEmpleadoViewModel());
        }

        private void PrepararViewBags()
        {
            ViewBag.Sucursales = new List<SelectListItem> { /* ... datos ... */ };
            ViewBag.Cargos = new List<SelectListItem> { /* ... datos ... */ };
            ViewBag.RolesDisponibles = new List<SelectListItem> { /* ... datos ... */ };
        }
    }
}