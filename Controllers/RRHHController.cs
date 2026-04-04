using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Data;
using RefrescosDelValle.Models.Entities;
using RefrescosDelValle.Models.ViewModels;

namespace RefrescosDelValle.Controllers
{
    [Authorize(Roles = "SuperAdmin,AdminRRHH")]
    public class RRHHController : BaseController
    {
        private readonly AppDbContext _context;

        public RRHHController(AppDbContext context)
        {
            _context = context;
        }

        // ══════════════════════════════════════════════════════════
        // EMPLEADOS
        // ══════════════════════════════════════════════════════════

        public async Task<IActionResult> Empleados(string? buscar)
        {
            var query = _context.Empleados
                .Include(e => e.Persona)
                .Include(e => e.Cargo)
                .Include(e => e.Departamento)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.ToLower();
                query = query.Where(e =>
                    e.Persona.Nombres.ToLower().Contains(buscar) ||
                    e.Persona.ApellidoPat.ToLower().Contains(buscar) ||
                    e.Persona.CI.Contains(buscar) ||
                    e.Cargo.NombreCargo.ToLower().Contains(buscar) ||
                    e.Departamento.NombreDepartamento.ToLower().Contains(buscar));
            }

            var empleados = await query.Select(e => new EmpleadoViewModel
            {
                EmpleadoID     = e.EmpleadoID,
                NombreCompleto = e.Persona.Nombres + " " + e.Persona.ApellidoPat +
                                 (e.Persona.ApellidoMat != null ? " " + e.Persona.ApellidoMat : ""),
                CI             = e.Persona.CI,
                Cargo          = e.Cargo.NombreCargo,
                Departamento   = e.Departamento.NombreDepartamento,
                Estado         = e.Estado,
                Salario        = e.Salario,
                FechaIngreso   = e.FechaIngreso,
                Correo         = e.Persona.CorreoPrincipal   // corregido
            }).ToListAsync();

            ViewBag.Buscar             = buscar;
            ViewBag.TotalEmpleados     = await _context.Empleados.CountAsync();
            ViewBag.TotalActivos       = await _context.Empleados.CountAsync(e => e.Estado == "Activo");
            ViewBag.TotalDepartamentos = await _context.DepartamentosEmpresa.CountAsync(d => d.Activo);

            return View(empleados);
        }

        public async Task<IActionResult> CrearEmpleado()
        {
            await CargarSelectLists();
            return View(new CrearEmpleadoViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearEmpleado(CrearEmpleadoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await CargarSelectLists();
                return View(vm);
            }

            // Verificar CI único
            if (await _context.Personas.AnyAsync(p => p.CI == vm.CI))
            {
                ModelState.AddModelError("CI", "Ya existe una persona registrada con ese CI.");
                await CargarSelectLists();
                return View(vm);
            }

            // Crear Persona — solo con los campos que existen en el modelo
            var persona = new Persona
            {
                Nombres          = vm.Nombres,
                ApellidoPat      = vm.ApellidoPat,
                ApellidoMat      = vm.ApellidoMat,
                CI               = vm.CI,
                CorreoPrincipal  = vm.Correo,   // corregido
                Estado           = "Activo",
                FechaRegistro    = DateTime.Now
            };
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            // Crear Empleado
            var empleado = new Empleado
            {
                PersonaID       = persona.PersonaID,
                CargoID         = vm.CargoID,
                DepartamentoID  = vm.DepartamentoID,
                SucursalID      = vm.SucursalID,
                FechaNacimiento = vm.FechaNacimiento,
                FechaIngreso    = vm.FechaIngreso,
                Salario         = vm.Salario,
                Estado          = "Activo"
            };
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            TempData["Exito"] = $"Empleado {persona.Nombres} {persona.ApellidoPat} registrado correctamente.";
            return RedirectToAction(nameof(Empleados));
        }

        public async Task<IActionResult> DetalleEmpleado(int id)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Persona)
                .Include(e => e.Cargo)
                .Include(e => e.Departamento)
                .Include(e => e.Asistencias)
                .Include(e => e.Planillas)
                .FirstOrDefaultAsync(e => e.EmpleadoID == id);

            if (empleado == null) return NotFound();
            return View(empleado);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleEstadoEmpleado(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return NotFound();

            empleado.Estado = empleado.Estado == "Activo" ? "Inactivo" : "Activo";
            await _context.SaveChangesAsync();

            TempData["Exito"] = $"Estado del empleado actualizado a {empleado.Estado}.";
            return RedirectToAction(nameof(Empleados));
        }

        // ══════════════════════════════════════════════════════════
        // ASISTENCIA
        // ══════════════════════════════════════════════════════════

        public async Task<IActionResult> Asistencia(string? buscar, DateOnly? fecha)
        {
            var fechaFiltro = fecha ?? DateOnly.FromDateTime(DateTime.Today);

            var query = _context.Asistencias
                .Include(a => a.Empleado).ThenInclude(e => e.Persona)
                .Where(a => a.Fecha == fechaFiltro)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.ToLower();
                query = query.Where(a =>
                    a.Empleado.Persona.Nombres.ToLower().Contains(buscar) ||
                    a.Empleado.Persona.ApellidoPat.ToLower().Contains(buscar));
            }

            var lista = await query.Select(a => new AsistenciaViewModel
            {
                AsistenciaID   = a.AsistenciaID,
                NombreEmpleado = a.Empleado.Persona.Nombres + " " + a.Empleado.Persona.ApellidoPat,
                Fecha          = a.Fecha,
                HoraEntrada    = a.HoraEntrada,
                HoraSalida     = a.HoraSalida,
                Estado         = a.Estado,
                Justificado    = a.Justificado,
                Observaciones  = a.Observaciones
            }).ToListAsync();

            ViewBag.FechaFiltro = fechaFiltro;
            ViewBag.Buscar      = buscar;
            ViewBag.Presentes   = lista.Count(a => a.Estado == "Presente");
            ViewBag.Ausentes    = lista.Count(a => a.Estado == "Ausente");
            ViewBag.Tardanzas   = lista.Count(a => a.Estado == "Tardanza");

            return View(lista);
        }

        public async Task<IActionResult> RegistrarAsistencia()
        {
            await CargarEmpleadosSelect();
            return View(new RegistrarAsistenciaViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarAsistencia(RegistrarAsistenciaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await CargarEmpleadosSelect();
                return View(vm);
            }

            if (await _context.Asistencias.AnyAsync(a => a.EmpleadoID == vm.EmpleadoID && a.Fecha == vm.Fecha))
            {
                ModelState.AddModelError("", "Ya existe un registro de asistencia para este empleado en esa fecha.");
                await CargarEmpleadosSelect();
                return View(vm);
            }

            var asistencia = new Asistencia
            {
                EmpleadoID    = vm.EmpleadoID,
                Fecha         = vm.Fecha,
                HoraEntrada   = vm.HoraEntrada,
                HoraSalida    = vm.HoraSalida,
                Estado        = vm.Estado,
                Observaciones = vm.Observaciones,
                Justificado   = vm.Justificado
            };
            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            TempData["Exito"] = "Asistencia registrada correctamente.";
            return RedirectToAction(nameof(Asistencia));
        }

        // ══════════════════════════════════════════════════════════
        // PLANILLA
        // ══════════════════════════════════════════════════════════

        public async Task<IActionResult> Planilla(int? anio, int? mes)
        {
            int anioFiltro = anio ?? DateTime.Now.Year;
            int mesFiltro  = mes  ?? DateTime.Now.Month;

            var lista = await _context.Planillas
                .Include(p => p.Empleado).ThenInclude(e => e.Persona)
                .Include(p => p.Empleado).ThenInclude(e => e.Cargo)
                .Where(p => p.Anio == anioFiltro && p.Mes == mesFiltro)
                .Select(p => new PlanillaViewModel
                {
                    PlanillaID     = p.PlanillaID,
                    NombreEmpleado = p.Empleado.Persona.Nombres + " " + p.Empleado.Persona.ApellidoPat,
                    Cargo          = p.Empleado.Cargo.NombreCargo,
                    Mes            = p.Mes,
                    Anio           = p.Anio,
                    HaberBasico    = p.HaberBasico,
                    Bonos          = p.Bonos,
                    Descuentos     = p.Descuentos,
                    TotalLiquido   = p.TotalLiquido,
                    Pagado         = p.Pagado,
                    FechaPago      = p.FechaPago
                }).ToListAsync();

            ViewBag.AnioFiltro   = anioFiltro;
            ViewBag.MesFiltro    = mesFiltro;
            ViewBag.TotalBruto   = lista.Sum(p => p.HaberBasico + p.Bonos);
            ViewBag.TotalLiquido = lista.Sum(p => p.TotalLiquido);
            ViewBag.Pagados      = lista.Count(p => p.Pagado);
            ViewBag.Pendientes   = lista.Count(p => !p.Pagado);

            return View(lista);
        }

        public async Task<IActionResult> GenerarPlanilla()
        {
            await CargarEmpleadosSelect();
            return View(new GenerarPlanillaViewModel { Mes = DateTime.Now.Month, Anio = DateTime.Now.Year });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerarPlanilla(GenerarPlanillaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await CargarEmpleadosSelect();
                return View(vm);
            }

            if (await _context.Planillas.AnyAsync(p => p.EmpleadoID == vm.EmpleadoID && p.Mes == vm.Mes && p.Anio == vm.Anio))
            {
                ModelState.AddModelError("", "Ya existe planilla para este empleado en ese mes/año.");
                await CargarEmpleadosSelect();
                return View(vm);
            }

            var planilla = new Planilla
            {
                EmpleadoID  = vm.EmpleadoID,
                Mes         = vm.Mes,
                Anio        = vm.Anio,
                HaberBasico = vm.HaberBasico,
                Bonos       = vm.Bonos,
                Descuentos  = vm.Descuentos,
                Pagado      = false
            };
            _context.Planillas.Add(planilla);
            await _context.SaveChangesAsync();

            var detalle = new PlanillaDetalle
            {
                PlanillaID = planilla.PlanillaID,
                AFP        = vm.AFP,
                RC_IVA     = vm.RC_IVA,
                CNS        = vm.CNS
            };
            _context.PlanillaDetalles.Add(detalle);
            await _context.SaveChangesAsync();

            TempData["Exito"] = "Planilla generada correctamente.";
            return RedirectToAction(nameof(Planilla));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarPagado(int id)
        {
            var planilla = await _context.Planillas.FindAsync(id);
            if (planilla == null) return NotFound();

            planilla.Pagado    = true;
            planilla.FechaPago = DateOnly.FromDateTime(DateTime.Today);
            await _context.SaveChangesAsync();

            TempData["Exito"] = "Planilla marcada como pagada.";
            return RedirectToAction(nameof(Planilla));
        }

        // ══════════════════════════════════════════════════════════
        // HELPERS PRIVADOS
        // ══════════════════════════════════════════════════════════

        private async Task CargarSelectLists()
        {
            ViewBag.Cargos = new SelectList(
                await _context.Cargos.Where(c => c.Activo).OrderBy(c => c.NombreCargo).ToListAsync(),
                "CargoID", "NombreCargo");

            ViewBag.Departamentos = new SelectList(
                await _context.DepartamentosEmpresa.Where(d => d.Activo).OrderBy(d => d.NombreDepartamento).ToListAsync(),
                "DepartamentoID", "NombreDepartamento");

            ViewBag.Sucursales = new SelectList(
                await _context.Sucursales.Where(s => s.Activo).OrderBy(s => s.NombreSucursal).ToListAsync(),
                "SucursalID", "NombreSucursal");
        }

        private async Task CargarEmpleadosSelect()
        {
            var empleados = await _context.Empleados
                .Include(e => e.Persona)
                .Where(e => e.Estado == "Activo")
                .OrderBy(e => e.Persona.ApellidoPat)
                .Select(e => new {
                    e.EmpleadoID,
                    Nombre = e.Persona.Nombres + " " + e.Persona.ApellidoPat
                })
                .ToListAsync();

            ViewBag.Empleados = new SelectList(empleados, "EmpleadoID", "Nombre");
        }
    }
}
