using System.ComponentModel.DataAnnotations;

namespace RefrescosDelValle.Models.ViewModels
{
    // ─── Empleados ───────────────────────────────────────────────
    public class EmpleadoViewModel
    {
        public int EmpleadoID { get; set; }
        public string NombreCompleto { get; set; } = "";
        public string CI { get; set; } = "";
        public string Cargo { get; set; } = "";
        public string Departamento { get; set; } = "";
        public string Estado { get; set; } = "";
        public decimal Salario { get; set; }
        public DateOnly FechaIngreso { get; set; }
        public string? Correo { get; set; }   // mapea a CorreoPrincipal
    }

    public class CrearEmpleadoViewModel
    {
        // Datos Persona
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [MaxLength(50)]
        public string ApellidoPat { get; set; } = null!;

        [MaxLength(50)]
        public string? ApellidoMat { get; set; }

        [Required(ErrorMessage = "El CI es obligatorio")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El CI debe tener exactamente 8 dígitos")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El CI solo debe contener números")]
        public string CI { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Correo inválido")]
        [MaxLength(150)]
        public string? Correo { get; set; }   // se guarda en CorreoPrincipal

        // Datos Empleado
        [Required(ErrorMessage = "El cargo es obligatorio")]
        public int CargoID { get; set; }

        [Required(ErrorMessage = "El departamento es obligatorio")]
        public int DepartamentoID { get; set; }

        [Required(ErrorMessage = "La sucursal es obligatoria")]
        public int SucursalID { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateOnly FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateOnly FechaIngreso { get; set; }

        [Required(ErrorMessage = "El salario es obligatorio")]
        [Range(0, 99999999, ErrorMessage = "Salario inválido")]
        public decimal Salario { get; set; }
    }

    // ─── Asistencia ───────────────────────────────────────────────
    public class AsistenciaViewModel
    {
        public int AsistenciaID { get; set; }
        public string NombreEmpleado { get; set; } = "";
        public DateOnly Fecha { get; set; }
        public TimeOnly HoraEntrada { get; set; }
        public TimeOnly? HoraSalida { get; set; }
        public string Estado { get; set; } = "";
        public bool Justificado { get; set; }
        public string? Observaciones { get; set; }
    }

    public class RegistrarAsistenciaViewModel
    {
        [Required(ErrorMessage = "El empleado es obligatorio")]
        public int EmpleadoID { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required(ErrorMessage = "La hora de entrada es obligatoria")]
        public TimeOnly HoraEntrada { get; set; }

        public TimeOnly? HoraSalida { get; set; }

        [Required]
        public string Estado { get; set; } = "Presente";

        [MaxLength(300)]
        public string? Observaciones { get; set; }

        public bool Justificado { get; set; } = false;
    }

    // ─── Planilla ─────────────────────────────────────────────────
    public class PlanillaViewModel
    {
        public int PlanillaID { get; set; }
        public string NombreEmpleado { get; set; } = "";
        public string Cargo { get; set; } = "";
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal HaberBasico { get; set; }
        public decimal Bonos { get; set; }
        public decimal Descuentos { get; set; }
        public decimal TotalLiquido { get; set; }
        public bool Pagado { get; set; }
        public DateOnly? FechaPago { get; set; }
        public string MesNombre => new DateTime(Anio, Mes, 1).ToString("MMMM");
    }

    public class GenerarPlanillaViewModel
    {
        [Required(ErrorMessage = "El empleado es obligatorio")]
        public int EmpleadoID { get; set; }

        [Required]
        [Range(1, 12, ErrorMessage = "Mes inválido")]
        public int Mes { get; set; }

        [Required]
        [Range(2000, 2100, ErrorMessage = "Año inválido")]
        public int Anio { get; set; } = DateTime.Now.Year;

        [Required]
        [Range(0.01, 99999999, ErrorMessage = "El haber básico debe ser mayor a 0")]
        public decimal HaberBasico { get; set; }

        [Range(0, 99999999)]
        public decimal Bonos { get; set; } = 0;

        [Range(0, 99999999)]
        public decimal Descuentos { get; set; } = 0;

        [Range(0, 99999999)]
        public decimal AFP { get; set; } = 0;

        [Range(0, 99999999)]
        public decimal RC_IVA { get; set; } = 0;

        [Range(0, 99999999)]
        public decimal CNS { get; set; } = 0;
    }
}
