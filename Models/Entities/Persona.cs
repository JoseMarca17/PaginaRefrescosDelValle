using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Persona")]
    public class Persona
    {
        [Key]
        public int PersonaID { get; set; }

        [Required, MaxLength(100)]
        public string Nombres { get; set; } = null!;

        [Required, MaxLength(50)]
        public string ApellidoPat { get; set; } = null!;

        [MaxLength(50)]
        public string? ApellidoMat { get; set; }

        [Required, StringLength(8)]
        public string CI { get; set; } = null!;

        [MaxLength(150)]
        public string? CorreoPrincipal { get; set; }

        [MaxLength(10)]
        public string Estado { get; set; } = "Activo";

        // --- LOS CAMPOS QUE FALTABAN ---
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }

        // Relación 1 a 1 con Usuario
        public virtual Usuario? Usuario { get; set; }
    }
}