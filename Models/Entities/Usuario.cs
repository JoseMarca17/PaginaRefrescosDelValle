using System.ComponentModel.DataAnnotations;

namespace RefrescosDelValle.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required, MaxLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Rol { get; set; } = "Usuario";

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}