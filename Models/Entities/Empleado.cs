using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Empleados")]
    public class Empleado
    {
        [Key]
        public int EmpleadoID { get; set; }

        [Required]
        public int PersonaID { get; set; }

        [Required]
        public int CargoID { get; set; }

        [Required]
        public int DepartamentoID { get; set; }

        [Required]
        public int SucursalID { get; set; }

        [Required]
        public DateOnly FechaNacimiento { get; set; }

        [Required]
        public DateOnly FechaIngreso { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Salario { get; set; } = 0;

        [Required, MaxLength(20)]
        public string Estado { get; set; } = "Activo";

        public int? SupervisorID { get; set; }
        public int? EstadoEmpleadoID { get; set; }

        // Navegación
        [ForeignKey("PersonaID")]
        public virtual Persona Persona { get; set; } = null!;

        [ForeignKey("CargoID")]
        public virtual Cargo Cargo { get; set; } = null!;

        [ForeignKey("DepartamentoID")]
        public virtual DepartamentoEmpresa Departamento { get; set; } = null!;

        [ForeignKey("SupervisorID")]
        public virtual Empleado? Supervisor { get; set; }

        public virtual ICollection<Asistencia> Asistencias { get; set; } = new List<Asistencia>();
        public virtual ICollection<Planilla> Planillas { get; set; } = new List<Planilla>();
        public virtual ICollection<Vacacion> Vacaciones { get; set; } = new List<Vacacion>();
    }
}
