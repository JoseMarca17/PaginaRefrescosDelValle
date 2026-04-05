using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RefrescosDelValle.Controllers
{
   //[Authorize]jajaputo
    public class InventarioController : Controller
    {
        public IActionResult IndexInventario()
        {
            return View();
        }

        public IActionResult Stock()
        {
            return View();
        }

        public IActionResult Almacenes()
        {
            return View();
        }

        public IActionResult Movimientos()
        {
            return View();
        }
    }
}
