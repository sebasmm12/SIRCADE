using SIRCADE.ApiCore.Controllers.Roles.Requests;
using SIRCADE.ApiCore.Controllers.Roles.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Queries;

namespace SIRCADE.ApiCore.Controllers.Roles.Services;

public interface IRolesService
{
    Task<IEnumerable<RoleResponse>> GetAsync();

    Task<DataTableDto<RoleResponse>> GetAsync(DataTableQueriesDto dataTableQueries);

    Task<RoleInfoResponse> GetAsync(int roleId);

    Task<int> CreateAsync(RoleCreationRequest roleCreationRequest);

    Task UpdateAsync(RoleUpdateRequest roleUpdateRequest);

    Task DeleteAsync(int roleId);

    Task UpdateStatusAsync(int roleId);
}