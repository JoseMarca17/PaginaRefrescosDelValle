namespace RefrescosDelValle.Models.ViewModels
{
    public class SucursalItemViewModel
    {
        public int SucursalId { get; set; }
        public string NombreSucursal { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string NombreCiudad { get; set; } = null!;
        public string NombreDepartamento { get; set; } = null!;
        public int TotalEmpleados { get; set; }
    }

    public class GeoResumenViewModel
    {
        public List<SucursalItemViewModel> Sucursales { get; set; } = new();
        public List<DepartamentoResumenViewModel> Departamentos { get; set; } = new();
        public int TotalSucursales { get; set; }
        public int TotalActivas { get; set; }
        public int TotalCiudades { get; set; }
        public int TotalDepartamentos { get; set; }
    }

    public class DepartamentoResumenViewModel
    {
        public int DepartamentoGeoId { get; set; }
        public string NombreDpto { get; set; } = null!;
        public List<CiudadResumenViewModel> Ciudades { get; set; } = new();
    }

    public class CiudadResumenViewModel
    {
        public int CiudadId { get; set; }
        public string NombreCiudad { get; set; } = null!;
        public int TotalSucursales { get; set; }
        public int TotalZonas { get; set; }
    }

    public class CrearSucursalViewModel
    {
        public string NombreSucursal { get; set; } = null!;
        public int CiudadId { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}
