using System;

namespace RefrescosDelValle.Models.Entities;

public partial class VwStockAlmacen
{
    public int? ProductoId { get; set; }
    public int? AlmacenId { get; set; }           
    public string? NombreProducto { get; set; }
    public string? CodigoSku { get; set; }
    public string? NombreAlmacen { get; set; }
    public string? NombreSucursal { get; set; }
    public decimal? CantidadDisponible { get; set; }
    public decimal? CantidadMinima { get; set; }
    public decimal? CostoUnitario { get; set; }  
    public string? EstadoAlmacen { get; set; }   
    public string? EstadoContenido { get; set; }  
    public DateTime? FechaActualizacion { get; set; } 
    public string? Lote { get; set; }
    public string? TipoAlmacen { get; set; }    
    public int? BajoMinimo { get; set; }
}