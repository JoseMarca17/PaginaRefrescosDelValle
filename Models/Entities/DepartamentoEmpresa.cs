using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("DepartamentosEmpresa")]
    public class DepartamentoEmpresa
    {
        [Key]
        public int DepartamentoID { get; set; }

        [Required, MaxLength(100)]
        public string NombreDepartamento { get; set; } = null!;

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        public bool Activo { get; set; } = true;

        public int? DepartamentoPadreID { get; set; }
        public int? JefeEmpleadoID { get; set; }

        // Navegación
        [ForeignKey("DepartamentoPadreID")]
        public virtual DepartamentoEmpresa? DepartamentoPadre { get; set; }

        [ForeignKey("JefeEmpleadoID")]
        public virtual Empleado? JefeEmpleado { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
