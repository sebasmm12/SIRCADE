using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Permissions.Entities;

namespace SIRCADE.ApiCore.Models.Permissions.Persistence.Imp;

public class GetPermissionsPersistence(ApplicationDbContext context) : IGetPermissionsPersistence
{
    public async Task<IEnumerable<Permission>> ExecuteAsync()
    {
        var permissions = await context
                                    .Permissions
                                    .AsNoTracking()
                                    .ToListAsync();

        return permissions;
    }

    public async Task<IEnumerable<Permission>> ExecuteAsync(int roleId)
    {
        var permissions = await context
                                    .RolePermissions
                                    .Include(rolePermission => rolePermission.Permission)
                                    .Where(rolePermission => rolePermission.RoleId == roleId)
                                    .AsNoTracking()
                                    .Select(rolePermission => rolePermission.Permission)
                                    .ToListAsync();

        return permissions;
    }
}