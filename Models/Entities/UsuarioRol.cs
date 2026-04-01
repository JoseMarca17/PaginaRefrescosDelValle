using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("UsuariosRoles")]
    public class UsuarioRol
    {
        [Key]
        public int UsuarioRolID { get; set; }

        public int UsuarioID { get; set; }
        public int RolID { get; set; }

        // --- EL CAMPO QUE FALTABA ---
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;

        [ForeignKey("RolID")]
        public virtual Rol Rol { get; set; } = null!;

        [ForeignKey("UsuarioID")]
        public virtual Usuario Usuario { get; set; } = null!;
    }
}