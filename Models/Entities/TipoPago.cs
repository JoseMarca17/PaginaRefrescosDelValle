using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaginaRefrescosDelValle.Models.Entities
{
    [Table("TipoPago")]
    public class TipoPago
    {
        [Key]
        public int TipoPagoID { get; set; }
        public string NombreTipo { get; set; } = null!;
        public bool Activo { get; set; } = true;
    }
}