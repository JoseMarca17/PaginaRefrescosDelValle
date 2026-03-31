using System.ComponentModel.DataAnnotations;

namespace RefrescosDelValle.Models.ViewModels
{
    public class VerificarOTPViewModel
    {
        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "El código debe tener 6 dígitos")]
        public string Codigo { get; set; } = string.Empty;

        public int IdUsuario { get; set; }
    }
}