namespace RefrescosDelValle.Models.ViewModels;

public class MovimientoViewModel
{
    public string Id { get; set; } = "";
    public string FechaEnvio { get; set; } = "";
    public string? FechaRecepcion { get; set; }
    public string Origen { get; set; } = "";
    public string Destino { get; set; } = "";
    public bool EsMerma { get; set; }
    public decimal Cantidad { get; set; }
    public string? Descripcion { get; set; }
    public string Tipo { get; set; } = "";
    public string? Transporte { get; set; } 
}
public class MovimientosPageViewModel
{
    public List<MovimientoViewModel> Movimientos { get; set; } = new();
    public int TotalMovimientos { get; set; }
    public int TotalTraslados { get; set; }
    public int TotalMermas { get; set; }
    public decimal TotalUnidadesMovidas { get; set; }
}