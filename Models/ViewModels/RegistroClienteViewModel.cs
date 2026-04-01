using System.ComponentModel.DataAnnotations;

namespace RefrescosDelValle.Models.ViewModels
{
    public class RegistroClienteViewModel
    {
        [Required(ErrorMessage = "El carnet es obligatorio.")]
        [StringLength(8, MinimumLength = 5, ErrorMessage = "El CI debe tener entre 5 y 8 dígitos.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El CI solo debe contener números.")]
        public string CI { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        public string ApellidoPat { get; set; } = null!;

        public string? ApellidoMat { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Debes confirmar tu contraseña.")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}