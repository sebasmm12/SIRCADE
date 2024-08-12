using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.RolePermissions.Entities;

namespace SIRCADE.ApiCore.Models.Roles.Entities;

[Table("Roles")]
public class Role
{
    public Role()
    {
        
    }

    public Role(string name, ICollection<RolePermission> permissions)
    {
        Name = name;
        Permissions = permissions;
        Active = true;
    }

    public int Id { get; set; }

    [Column("Nombre")]
    public string Name { get; set; } = default!;

    [Column("Activo")]
    public bool Active { get; set; }

    public ICollection<RolePermission> Permissions { get; set; } = [];
}