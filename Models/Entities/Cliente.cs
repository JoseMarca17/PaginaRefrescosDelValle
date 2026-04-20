using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaginaRefrescosDelValle.Models.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        // Relación con TipoCliente
        public int TipoClienteID { get; set; }
        
        [ForeignKey("TipoClienteID")]
        public virtual TipoCliente TipoCliente { get; set; }
    }
}