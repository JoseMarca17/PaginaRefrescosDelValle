using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Vacaciones")]
    public class Vacacion
    {
        [Key]
        public int VacacionID { get; set; }

        [Required]
        public int EmpleadoID { get; set; }

        [Required]
        public DateOnly FechaInicio { get; set; }

        [Required]
        public DateOnly FechaFin { get; set; }

        [Required]
        public int DiasSolicitados { get; set; }

        [Required, MaxLength(20)]
        public string Estado { get; set; } = "Pendiente";

        public int? AprobadoPorID { get; set; }

        [MaxLength(300)]
        public string? Observaciones { get; set; }

        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        // Navegación
        [ForeignKey("EmpleadoID")]
        public virtual Empleado Empleado { get; set; } = null!;

        [ForeignKey("AprobadoPorID")]
        public virtual Usuario? AprobadoPor { get; set; }
    }
}
