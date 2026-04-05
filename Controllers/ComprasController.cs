using Microsoft.AspNetCore.Mvc;

namespace MiProyecto.Controllers
{
    public class ComprasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}