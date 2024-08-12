using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Queries;
using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Models.Roles.Persistence;

public interface IGetRolesPersistence
{
    Task<IEnumerable<Role>> ExecuteAsync();

    Task<Role> ExecuteAsync(int roleId, bool needsInclude = false, bool isTracked = false);

    Task<DataTableDto<Role>> ExecuteAsync(DataTableQueriesDto dataTableQueries);
}