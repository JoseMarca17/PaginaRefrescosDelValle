using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Planilla")]
    public class Planilla
    {
        [Key]
        public int PlanillaID { get; set; }

        [Required]
        public int EmpleadoID { get; set; }

        [Required]
        public int Mes { get; set; }

        [Required]
        public int Anio { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal HaberBasico { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Bonos { get; set; } = 0;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Descuentos { get; set; } = 0;

        // Calculado en BD: HaberBasico + Bonos - Descuentos
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalLiquido { get; set; }

        public DateOnly? FechaPago { get; set; }

        public bool Pagado { get; set; } = false;

        // Navegación
        [ForeignKey("EmpleadoID")]
        public virtual Empleado Empleado { get; set; } = null!;

        public virtual PlanillaDetalle? Detalle { get; set; }
    }
}
