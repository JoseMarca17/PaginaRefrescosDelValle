using System.ComponentModel.DataAnnotations;

namespace RefrescosDelValle.Models.ViewModels
{
    public class EditarSucursalViewModel
    {
        public int SucursalId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string NombreSucursal { get; set; } = "";

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public int CiudadId { get; set; }

        [MaxLength(200)]
        public string? Direccion { get; set; }

        [MaxLength(15)]
        public string? Telefono { get; set; }
    }
}