using SIRCADE.ApiCore.Controllers.Permissions.Responses;
using SIRCADE.ApiCore.Models.Permissions.Entities;

namespace SIRCADE.ApiCore.Controllers.Permissions.Mappers;

public static class PermissionsMapper
{
    public static PermissionInfoResponse MapToPermissionInfoResponse(this Permission permission)
    {
        return new(permission.Id, permission.Name);
    }
}