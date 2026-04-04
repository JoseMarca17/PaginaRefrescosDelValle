using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("PlanillaDetalle")]
    public class PlanillaDetalle
    {
        [Key]
        public int DetalleID { get; set; }

        [Required]
        public int PlanillaID { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal AFP { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal RC_IVA { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal CNS { get; set; }

        // Navegación
        [ForeignKey("PlanillaID")]
        public virtual Planilla Planilla { get; set; } = null!;
    }
}
