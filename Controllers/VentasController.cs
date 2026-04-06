using Microsoft.AspNetCore.Mvc;

namespace MiProyecto.Controllers
{
    public class VentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NuevoPedido()
        {
            return View();
        }
    }
}