using SIRCADE.ApiCore.Controllers.Permissions.Responses;

namespace SIRCADE.ApiCore.Controllers.Permissions.Services;

public interface IPermissionsService
{
    Task<IEnumerable<PermissionInfoResponse>> GetAsync();

    Task<IEnumerable<RolePermissionResponse>> GetAsync(int roleId);
}