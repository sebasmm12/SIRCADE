using SIRCADE.ApiCore.Controllers.Permissions.Mappers;
using SIRCADE.ApiCore.Controllers.Permissions.Responses;
using SIRCADE.ApiCore.Models.Permissions.Persistence;

namespace SIRCADE.ApiCore.Controllers.Permissions.Services.Imp;

public class PermissionsService
    (IGetPermissionsPersistence getPermissionsPersistence): IPermissionsService
{
    public async Task<IEnumerable<PermissionInfoResponse>> GetAsync()
    {
        var permissions = await getPermissionsPersistence.ExecuteAsync();

        var permissionsResponse = permissions.Select(permission => permission.MapToPermissionInfoResponse());

        return permissionsResponse;
    }
}