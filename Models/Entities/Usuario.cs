using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int UsuarioID { get; set; }

        [Required]
        public int PersonaID { get; set; }

        [Required, MaxLength(50)]
        public string NombreUsuario { get; set; } = null!;

        [Required, MaxLength(255)]
        [Column("Contrasena")] // Le decimos a EF que en la BD se llama 'Contrasena'
        public string PasswordHash { get; set; } = null!;

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? UltimoAcceso { get; set; }

        // Navegación
        [ForeignKey("PersonaID")]
        public virtual Persona Persona { get; set; } = null!;
    }
}