using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaginaRefrescosDelValle.Data;
using PaginaRefrescosDelValle.Models.Entities;

namespace PaginaRefrescosDelValle.Controllers
{
    public class VentasController : Controller
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        // Listado de Ventas Realizadas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                .Include(v => v.Cliente)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();
            return View(ventas);
        }
    }
}