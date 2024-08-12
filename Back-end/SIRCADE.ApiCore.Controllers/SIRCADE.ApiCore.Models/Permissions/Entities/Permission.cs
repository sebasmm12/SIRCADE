using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.Permissions.Enums;
using SIRCADE.ApiCore.Models.RolePermissions.Entities;

namespace SIRCADE.ApiCore.Models.Permissions.Entities;

[Table("Permisos")]
public class Permission
{
    public Permission()
    {
        
    }

    public Permission(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    [Column("Nombre")]
    public string Name { get; set; } = default!;

    [Column("Descripcion")]
    public string Description { get; set; } = default!;

    [Column("Tipo")]
    public PermissionType Type { get; set; }

    [Column("Icono")]
    public string? Icon { get; set; }

    public ICollection<RolePermission> Roles { get; set; } = [];
}