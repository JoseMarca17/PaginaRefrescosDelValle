using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RefrescosDelValle.Controllers;

/// <summary>
/// Controlador para las páginas públicas del sitio web institucional.
/// Todas las acciones son accesibles sin autenticación.
/// </summary>
[AllowAnonymous]
public class PaginaController : Controller
{
    public IActionResult Productos()
    {
        ViewData["Title"] = "Productos";
        return View();
    }

    public IActionResult Nosotros()
    {
        ViewData["Title"] = "Nosotros";
        return View();
    }

    public IActionResult Contacto()
    {
        ViewData["Title"] = "Contacto";
        return View();
    }
}