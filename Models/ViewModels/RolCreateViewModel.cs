// ViewModels/RolCreateViewModel.cs
using System.ComponentModel.DataAnnotations;
namespace RefrescosDelValle.Models.ViewModels {
public class RolCreateViewModel
{
    public int RolId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100)]
    public string NombreRol { get; set; } = "";

    [MaxLength(300)]
    public string? Descripcion { get; set; }

    // IDs de permisos seleccionados
    public List<int> PermisosSeleccionados { get; set; } = new();

    // Para renderizar el formulario agrupado por módulo
    public List<ModuloPermisosVM> Modulos { get; set; } = new();
}

public class ModuloPermisosVM
{
    public string NombreModulo { get; set; } = "";
    public List<PermisoItemVM> Permisos { get; set; } = new();
}

public class PermisoItemVM
{
    public int PermisoId { get; set; }
    public string Accion { get; set; } = "";
    public string NombrePermiso { get; set; } = "";
    public bool Seleccionado { get; set; }
}
}