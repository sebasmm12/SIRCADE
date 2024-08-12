using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Queries;
using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Models.Roles.Persistence.Imp;

public class GetRolesPersistence(ApplicationDbContext context) : IGetRolesPersistence
{
    public async Task<IEnumerable<Role>> ExecuteAsync()
    {
        var roles = await context
                            .Roles
                            .Include(role => role.Permissions)
                            .AsNoTracking()
                            .ToListAsync();

        return roles;
    }

    public async Task<DataTableDto<Role>> ExecuteAsync(DataTableQueriesDto dataTableQueries)
    {
        var roles = await context
                            .Roles
                            .Include(role => role.Permissions)
                            .AsNoTracking()
                            .Skip(dataTableQueries.Page)
                            .Take(dataTableQueries.PageSize)
                            .ToListAsync();

        var totalRoles = await context.Roles.CountAsync();

        return new(roles, totalRoles);
    }

    public async Task<Role> ExecuteAsync(int roleId, bool needsInclude = false, bool isTracked = false)
    {
        var rolesContext = context.Roles.AsQueryable();

        if (needsInclude)
            rolesContext = rolesContext.Include(role => role.Permissions);

        if (isTracked)
            rolesContext = rolesContext.AsTracking();

        var role = await rolesContext.FirstOrDefaultAsync(role => role.Id == roleId);

        ArgumentNullException.ThrowIfNull(role);

        return role;
    }
}