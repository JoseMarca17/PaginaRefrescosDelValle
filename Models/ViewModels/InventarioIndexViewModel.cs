namespace RefrescosDelValle.Models.ViewModels;

public class InventarioIndexViewModel
{
    // ── Hero ──
    public int TotalUnidades { get; set; }
    public int TotalAlmacenes { get; set; }
    public int TotalDepartamentos { get; set; }

    // ── Card Stock ──
    public int TotalSKUs { get; set; }
    public int SKUsCriticos { get; set; }   // productos con stock <= 50 unidades

    // ── Card Almacenes ──
    public int AlmacenesActivos { get; set; }
    public int AlmacenesEnBaja { get; set; }

    // ── Card Movimientos ──
    public int MovimientosEsteMes { get; set; }
    public int TrasladosEsteMes { get; set; }
    public int MermasEsteMes { get; set; }
}
