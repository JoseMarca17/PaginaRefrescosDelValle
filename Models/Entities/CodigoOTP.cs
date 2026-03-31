using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    public class CodigoOTP
    {
        [Key]
        public int IdCodigo { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [Required, MaxLength(6)]
        public string Codigo { get; set; } = string.Empty;

        public DateTime FechaExpiracion { get; set; }

        public bool Usado { get; set; } = false;

        public virtual Usuario? Usuario { get; set; }
    }
}