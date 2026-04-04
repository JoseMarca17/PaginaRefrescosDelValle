using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RefrescosDelValle.Models.Entities
{
    [Table("Sucursales")]
    public class Sucursal
    {
        [Key]
        public int SucursalID { get; set; }

        [Required, MaxLength(100)]
        public string NombreSucursal { get; set; } = null!;

        [MaxLength(200)]
        public string? Direccion { get; set; }

        [MaxLength(15)]
        public string? Telefono { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        // Navegación
        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
