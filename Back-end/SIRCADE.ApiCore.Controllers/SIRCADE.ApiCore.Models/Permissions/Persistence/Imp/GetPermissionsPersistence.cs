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
}