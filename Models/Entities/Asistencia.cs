using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Asistencia")]
    public class Asistencia
    {
        [Key]
        public int AsistenciaID { get; set; }

        [Required]
        public int EmpleadoID { get; set; }

        [Required]
        public DateOnly Fecha { get; set; }

        [Required]
        public TimeOnly HoraEntrada { get; set; }

        public TimeOnly? HoraSalida { get; set; }

        [Required, MaxLength(20)]
        public string Estado { get; set; } = "Presente";

        [MaxLength(300)]
        public string? Observaciones { get; set; }

        public bool Justificado { get; set; } = false;

        public int? EstadoAsistenciaID { get; set; }

        // Navegación
        [ForeignKey("EmpleadoID")]
        public virtual Empleado Empleado { get; set; } = null!;
    }
}
