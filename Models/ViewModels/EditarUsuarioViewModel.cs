using System.ComponentModel.DataAnnotations;

namespace RefrescosDelValle.Models.ViewModels
{
    public class EditarUsuarioViewModel
    {
        public int UsuarioId { get; set; }

        // ── Datos de Persona ──
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [StringLength(50)]
        public string ApellidoPat { get; set; } = null!;

        [StringLength(50)]
        public string? ApellidoMat { get; set; }

        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string? CorreoPrincipal { get; set; }

        [StringLength(20)]
        public string? TelefonoPrincipal { get; set; }

        // ── Datos de Usuario ──
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(100)]
        public string NombreUsuario { get; set; } = null!;

        // Contraseña opcional al editar (si viene vacía, no se cambia)
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mínimo 6 caracteres")]
        public string? NuevaContrasena { get; set; }

        public bool Activo { get; set; }

        // ── Roles asignados ──
        public List<int> RolesSeleccionados { get; set; } = new();

        // ── Para mostrar en la vista ──
        public string Iniciales { get; set; } = "??";
        public string NombreCompleto => $"{Nombres} {ApellidoPat}";
    }
}
