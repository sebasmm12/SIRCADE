using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.Permissions.Entities;
using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Models.RolePermissions.Entities;

[Table("PermisosRol")]
public class RolePermission
{
    public RolePermission()
    {
        
    }

    public RolePermission(int permissionId)
    {
        PermissionId = permissionId;
    }

    [Column("IdRol")]
    public int RoleId { get; set; }

    [Column("IdPermiso")]
    public int PermissionId { get; set; }

    public Role Role { get; set; } = default!;

    public Permission Permission { get; set; } = default!;
}