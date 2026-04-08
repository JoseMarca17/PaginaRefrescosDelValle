namespace RefrescosDelValle.Models.ViewModels
{
    public class AuditoriaViewModel
    {
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; } = "Sistema";
        public string Operacion { get; set; } = null!;
        public string Tabla { get; set; } = null!;
        public string Detalle { get; set; } = null!;
    }
}