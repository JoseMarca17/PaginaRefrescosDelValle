using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaginaRefrescosDelValle.Data;
using PaginaRefrescosDelValle.Models.Entities;

namespace PaginaRefrescosDelValle.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // --- 1. LISTADO (Index) ---
        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes
                .Include(c => c.TipoCliente)
                .ToListAsync();
            return View(clientes);
        }

        // --- 2. VER DETALLES (Details) ---
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Clientes
                .Include(c => c.TipoCliente)
                .FirstOrDefaultAsync(m => m.ClienteID == id);

            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // --- 3. CREAR CLIENTE (GET) ---
        public IActionResult Create()
        {
            ViewData["TipoClienteID"] = new SelectList(_context.TipoClientes, "TipoClienteID", "Descripcion");
            return View();
        }

        // --- 4. CREAR CLIENTE (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoClienteID"] = new SelectList(_context.TipoClientes, "TipoClienteID", "Descripcion", cliente.TipoClienteID);
            return View(cliente);
        }

        // --- 5. ELIMINAR CLIENTE (GET - Confirmación) ---
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var cliente = await _context.Clientes
                .Include(c => c.TipoCliente)
                .FirstOrDefaultAsync(m => m.ClienteID == id);

            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // --- 6. ELIMINAR CLIENTE (POST - Acción Real) ---
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}