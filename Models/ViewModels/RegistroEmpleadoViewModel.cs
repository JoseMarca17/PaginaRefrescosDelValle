using System.ComponentModel.DataAnnotations;

namespace RefrescosDelValle.Models.ViewModels
{
    public class RegistroEmpleadoViewModel
    {
        [Required] public string CI { get; set; } = null!;
        [Required] public string Nombres { get; set; } = null!;
        [Required] public string ApellidoPat { get; set; } = null!;
        [Required, EmailAddress] public string Email { get; set; } = null!;
        [Required, MinLength(8)] public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Debe asignar una sucursal")]
        public int SucursalID { get; set; }

        [Required(ErrorMessage = "Debe asignar un cargo")]
        public int CargoID { get; set; }

        public List<int> RolesSeleccionados { get; set; } = new();
    }
}