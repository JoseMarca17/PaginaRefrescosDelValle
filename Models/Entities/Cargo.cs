using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Cargos")]
    public class Cargo
    {
        [Key]
        public int CargoID { get; set; }

        [Required, MaxLength(100)]
        public string NombreCargo { get; set; } = null!;

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal SalarioBase { get; set; } = 0;

        public bool Activo { get; set; } = true;

        public int? CargoPadreID { get; set; }

        // Navegación
        [ForeignKey("CargoPadreID")]
        public virtual Cargo? CargoPadre { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
