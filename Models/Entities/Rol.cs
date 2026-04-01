using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Roles")]
    public class Rol
    {
        [Key]
        public int RolID { get; set; }

        [Required, MaxLength(50)]
        public string NombreRol { get; set; } = null!;

        public bool Activo { get; set; } = true;
    }
}