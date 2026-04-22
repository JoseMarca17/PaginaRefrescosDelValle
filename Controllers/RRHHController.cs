using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefrescosDelValle.Models.Entities; 
using RefrescosDelValle.Models.ViewModels;

namespace RefrescosDelValle.Controllers
{
    [Authorize(Roles = "SuperAdmin,AdminRRHH")]
    public class RRHHController : Controller 
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
                    e.Persona.NumeroDocumento.Contains(buscar) || 
                    e.Cargo.NombreCargo.ToLower().Contains(buscar) ||
                    e.Departamento.NombreDepartamento.ToLower().Contains(buscar));
            }

            var empleados = await query.Select(e => new EmpleadoViewModel
            {
                EmpleadoID     = e.EmpleadoId,
                NombreCompleto = e.Persona.Nombres + " " + e.Persona.ApellidoPat +
                                 (e.Persona.ApellidoMat != null ? " " + e.Persona.ApellidoMat : ""),
                CI             = e.Persona.NumeroDocumento, 
                Cargo          = e.Cargo.NombreCargo,
                Departamento   = e.Departamento.NombreDepartamento,
                Estado         = e.Persona.Estado,
                Salario        = e.Salario,
                FechaIngreso   = e.FechaIngreso,
                Correo         = e.Persona.CorreoPrincipal
            }).ToListAsync();

            ViewBag.Buscar             = buscar;
            ViewBag.TotalEmpleados     = await _context.Empleados.CountAsync();
            ViewBag.TotalActivos       = await _context.Empleados.CountAsync(e => e.Persona.Estado == "Activo");
            ViewBag.TotalDepartamentos = await _context.DepartamentosEmpresas.CountAsync(d => d.Activo);

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

            if (await _context.Personas.AnyAsync(p => p.NumeroDocumento == vm.CI))
            {
                ModelState.AddModelError("CI", "Ya existe una persona registrada con ese CI.");
                await CargarSelectLists();
                return View(vm);
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var tipoDocCI       = await _context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 10 && d.Descripcion == "Carnet de Identidad");
                var sexoIndet       = await _context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 1  && d.Descripcion == "Indeterminado");
                var estadoEmpActivo = await _context.DominioValors.FirstOrDefaultAsync(d => d.DominioTipoId == 20 && d.Descripcion == "Activo");

                var persona = new Persona
                {
                    Nombres         = vm.Nombres,
                    ApellidoPat     = vm.ApellidoPat,
                    ApellidoMat     = vm.ApellidoMat,
                    NumeroDocumento = vm.CI,
                    TipoDocumentoId = tipoDocCI?.DominioValorId ?? 1,
                    SexoId          = sexoIndet?.DominioValorId ?? 1,
                    CorreoPrincipal = vm.Correo,
                    Estado          = "Activo",
                    FechaNacimiento = vm.FechaNacimiento 
                };
                _context.Personas.Add(persona);
                await _context.SaveChangesAsync();

                var empleado = new Empleado
                {
                    PersonaId        = persona.PersonaId,
                    CargoId          = vm.CargoID,
                    DepartamentoId   = vm.DepartamentoID,
                    SucursalId       = vm.SucursalID,
                    FechaIngreso     = vm.FechaIngreso,
                    Salario          = vm.Salario,
                    EstadoEmpleadoId = estadoEmpActivo?.DominioValorId ?? 1
                };
                _context.Empleados.Add(empleado);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                TempData["Exito"] = $"Empleado {persona.Nombres} {persona.ApellidoPat} registrado correctamente.";
                return RedirectToAction(nameof(Empleados));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Error al registrar: " + ex.Message);
                await CargarSelectLists();
                return View(vm);
            }
        }

        public async Task<IActionResult> DetalleEmpleado(int id)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Persona)
                .Include(e => e.Cargo)
                .Include(e => e.Departamento)
                .FirstOrDefaultAsync(e => e.EmpleadoId == id);

            if (empleado == null) return NotFound();

            ViewBag.Asistencias = await _context.Asistencia
                .Where(a => a.EmpleadoId == id).Take(10).ToListAsync();
            ViewBag.Planillas   = await _context.Planillas
                .Where(p => p.EmpleadoId == id).ToListAsync();

            return View(empleado);
        }

        public async Task<IActionResult> EditarEmpleado(int id)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(e => e.EmpleadoId == id);

            if (empleado == null) return NotFound();

            var vm = new EditarEmpleadoViewModel
            {
                EmpleadoID      = empleado.EmpleadoId,
                Nombres         = empleado.Persona.Nombres,
                ApellidoPat     = empleado.Persona.ApellidoPat,
                ApellidoMat     = empleado.Persona.ApellidoMat,
                CI              = empleado.Persona.NumeroDocumento,
                Correo          = empleado.Persona.CorreoPrincipal,
                FechaNacimiento = empleado.Persona.FechaNacimiento,
                CargoID         = empleado.CargoId,
                DepartamentoID  = empleado.DepartamentoId,
                SucursalID      = empleado.SucursalId,
                Salario         = empleado.Salario,
                FechaIngreso    = empleado.FechaIngreso
            };

            await CargarSelectLists();
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarEmpleado(int id, EditarEmpleadoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await CargarSelectLists();
                return View(vm);
            }

            var empleado = await _context.Empleados
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(e => e.EmpleadoId == id);

            if (empleado == null) return NotFound();

            if (await _context.Personas.AnyAsync(p => p.NumeroDocumento == vm.CI && p.PersonaId != empleado.PersonaId))
            {
                ModelState.AddModelError("CI", "Ya existe otra persona con ese CI.");
                await CargarSelectLists();
                return View(vm);
            }

            empleado.Persona.Nombres         = vm.Nombres;
            empleado.Persona.ApellidoPat     = vm.ApellidoPat;
            empleado.Persona.ApellidoMat     = vm.ApellidoMat;
            empleado.Persona.NumeroDocumento = vm.CI;
            empleado.Persona.CorreoPrincipal = vm.Correo;
            empleado.Persona.FechaNacimiento = vm.FechaNacimiento;
            empleado.CargoId                 = vm.CargoID;
            empleado.DepartamentoId          = vm.DepartamentoID;
            empleado.SucursalId              = vm.SucursalID;
            empleado.Salario                 = vm.Salario;
            empleado.FechaIngreso = vm.FechaIngreso ?? DateOnly.MinValue;

            await _context.SaveChangesAsync();

            TempData["Exito"] = $"Empleado {vm.Nombres} {vm.ApellidoPat} actualizado correctamente.";
            return RedirectToAction(nameof(Empleados));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleEstadoEmpleado(int id)
        {
            var empleado = await _context.Empleados
                .Include(e => e.Persona)
                .FirstOrDefaultAsync(e => e.EmpleadoId == id);
            if (empleado == null) return NotFound();

            empleado.Persona.Estado = empleado.Persona.Estado == "Activo" ? "Baja" : "Activo";
            await _context.SaveChangesAsync();

            TempData["Exito"] = $"Estado del empleado actualizado a {empleado.Persona.Estado}.";
            return RedirectToAction(nameof(Empleados));
        }

        // ══════════════════════════════════════════════════════════
        // ASISTENCIA
        // ══════════════════════════════════════════════════════════

        public async Task<IActionResult> Asistencia(string? buscar, DateOnly? fecha)
        {
            var fechaFiltro = fecha ?? DateOnly.FromDateTime(DateTime.Today);

            var query = _context.Asistencia
                .Include(a => a.Empleado).ThenInclude(e => e.Persona)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                buscar = buscar.ToLower();
                query = query.Where(a =>
                    a.Empleado.Persona.Nombres.ToLower().Contains(buscar) ||
                    a.Empleado.Persona.ApellidoPat.ToLower().Contains(buscar));
            }

            var dominios = await _context.DominioValors
                .Where(d => d.DominioTipoId == 21)
                .ToDictionaryAsync(d => d.DominioValorId, d => d.Descripcion);

            var listaResultados = await query.ToListAsync();

            var lista = listaResultados
                .Where(a => a.Fecha.ToString("yyyy-MM-dd") == fechaFiltro.ToString("yyyy-MM-dd"))
                .Select(a => new AsistenciaViewModel
                {
                    AsistenciaID   = a.AsistenciaId,
                    NombreEmpleado = a.Empleado.Persona.Nombres + " " + a.Empleado.Persona.ApellidoPat,
                    Fecha          = a.Fecha,
                    HoraEntrada    = a.HoraEntrada,
                    HoraSalida     = a.HoraSalida,
                    Estado         = dominios.ContainsKey(a.EstadoAsistenciaId) ? dominios[a.EstadoAsistenciaId] : "Desconocido",
                    Justificado    = a.Justificado,
                    Observaciones  = a.Observaciones
                }).ToList();

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

            if (await _context.Asistencia.AnyAsync(a => a.EmpleadoId == vm.EmpleadoID && a.Fecha == vm.Fecha))
            {
                ModelState.AddModelError("", "Ya existe un registro de asistencia para este empleado en esa fecha.");
                await CargarEmpleadosSelect();
                return View(vm);
            }

            var estadoDom = await _context.DominioValors
                .FirstOrDefaultAsync(d => d.DominioTipoId == 21 && d.Descripcion == vm.Estado);

            var asistencia = new Asistencium
            {
                EmpleadoId         = vm.EmpleadoID,
                Fecha              = vm.Fecha,
                HoraEntrada        = vm.HoraEntrada,
                HoraSalida         = vm.HoraSalida,
                EstadoAsistenciaId = estadoDom?.DominioValorId ?? 1,
                Observaciones      = vm.Observaciones,
                Justificado        = vm.Justificado
            };
            _context.Asistencia.Add(asistencia);
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
                    PlanillaID     = p.PlanillaId,
                    NombreEmpleado = p.Empleado.Persona.Nombres + " " + p.Empleado.Persona.ApellidoPat,
                    Cargo          = p.Empleado.Cargo.NombreCargo,
                    Mes            = p.Mes,
                    Anio           = p.Anio,
                    HaberBasico    = p.HaberBasico,
                    Bonos          = p.Bonos,
                    Descuentos     = p.Descuentos,
                    TotalLiquido   = p.TotalLiquido ?? 0,
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

            if (await _context.Planillas.AnyAsync(p => p.EmpleadoId == vm.EmpleadoID && p.Mes == vm.Mes && p.Anio == vm.Anio))
            {
                ModelState.AddModelError("", "Ya existe planilla para este empleado en ese mes/año.");
                await CargarEmpleadosSelect();
                return View(vm);
            }

            var planilla = new Planilla
            {
                EmpleadoId  = vm.EmpleadoID,
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
                PlanillaId = planilla.PlanillaId,
                Afp        = vm.AFP,
                RcIva      = vm.RC_IVA,
                Cns        = vm.CNS
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
                await _context.Cargos
                    .Where(c => c.Activo).OrderBy(c => c.NombreCargo).ToListAsync(),
                "CargoId", "NombreCargo");

            ViewBag.Departamentos = new SelectList(
                await _context.DepartamentosEmpresas
                    .Where(d => d.Activo).OrderBy(d => d.NombreDepartamento).ToListAsync(),
                "DepartamentoId", "NombreDepartamento");

            ViewBag.Sucursales = new SelectList(
                await _context.Sucursales
                    .Where(s => s.Activo).OrderBy(s => s.NombreSucursal).ToListAsync(),
                "SucursalId", "NombreSucursal");
        }

        private async Task CargarEmpleadosSelect()
        {
            var empleados = await _context.Empleados
                .Include(e => e.Persona)
                .Where(e => e.Persona.Estado == "Activo")
                .OrderBy(e => e.Persona.ApellidoPat)
                .Select(e => new {
                    e.EmpleadoId,
                    Nombre = e.Persona.Nombres + " " + e.Persona.ApellidoPat
                })
                .ToListAsync();

            ViewBag.Empleados = new SelectList(empleados, "EmpleadoId", "Nombre");
        }
    }
}