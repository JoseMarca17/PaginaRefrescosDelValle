namespace RefrescosDelValle.Models.ViewModels
{
    public class UsuarioItemViewModel
    {
        public int UsuarioID { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Roles { get; set; } = null!;
        public string Sucursal { get; set; } = "Sin asignar";
        public bool Activo { get; set; }
        public string Iniciales { get; set; } = "??";
    }
}