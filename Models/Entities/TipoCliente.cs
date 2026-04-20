using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PaginaRefrescosDelValle.Models.Entities
{
    public class TipoCliente
    {
        [Key]
        public int TipoClienteID { get; set; }

        [Required]
        public string Descripcion { get; set; }

        // Relación inversa
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}