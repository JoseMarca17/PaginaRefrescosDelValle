// Models/Enums/RolUsuario.cs
namespace RefrescosDelValle.Models.Enums
{
    public enum RolUsuario
    {
        SuperAdmin = 1,      // Acceso total a todo
        AdminSeguridad = 2,   // Gestión de usuarios, roles, sucursales
        AdminProduccion = 3,   // Gestión de producción, recetas, líneas
        AdminInventario = 4,   // Gestión de stock, almacenes, movimientos
        AdminVentas = 5,       // Gestión de pedidos, clientes, facturación
        AdminCompras = 6,      // Gestión de proveedores, compras, logística
        AdminRRHH = 7,         // Gestión de empleados, planilla, asistencia
        Supervisor = 8,        // Vista parcial de varios módulos
        Operador = 9,          // Solo operaciones específicas
        Cliente = 10           // Solo tienda y sus pedidos
    }
}