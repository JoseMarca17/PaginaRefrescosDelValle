using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaginaRefrescosDelValle.Models.Entities
{
    [Table("EstadoFactura")]
    public class EstadoFactura
    {
        [Key]
        public int EstadoFacturaID { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreEstado { get; set; } = null!;

        public bool Activo { get; set; } = true;
    }
}