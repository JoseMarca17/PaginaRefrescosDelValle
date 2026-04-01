using System.ComponentModel.DataAnnotations;

namespace RefrescosDelValle.Models.ViewModels
{
    public class RegistroEmpleadoViewModel
    {
        // --- DATOS PERSONALES ---
        [Required(ErrorMessage = "El carnet es obligatorio.")]
        public string CI { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombres { get; set; } = null!;

        [Required(ErrorMessage = "El apellido paterno es obligatorio.")]
        public string ApellidoPat { get; set; } = null!;

        public string? ApellidoMat { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        // --- CREDENCIALES ---
        [Required, MinLength(8, ErrorMessage = "Mínimo 8 caracteres para el hash.")]
        public string Password { get; set; } = null!;

        // --- DATOS CORPORATIVOS ---
        [Required(ErrorMessage = "Debes asignar una sucursal.")]
        public int SucursalID { get; set; }

        [Required(ErrorMessage = "Debes asignar un cargo operativo.")]
        public int CargoID { get; set; }

        // Aquí guardaremos los IDs de los roles que elijas en los checkboxes (Ej: AdminVentas, AdminInventario)
        [Required(ErrorMessage = "El nodo debe tener al menos un rol de seguridad.")]
        public List<int> RolesSeleccionados { get; set; } = new List<int>();
    }
}