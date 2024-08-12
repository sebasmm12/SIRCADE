using SIRCADE.ApiCore.Controllers.Roles.Requests;
using SIRCADE.ApiCore.Controllers.Roles.Responses;
using SIRCADE.ApiCore.Models.RolePermissions.Entities;
using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Controllers.Roles.Mappers;

public static class RolesMapper
{

    public static RoleResponse ToRoleResponse(this Role role)
    {
        return new(role.Id, role.Name, role.Permissions.Count);
    }

    public static Role ToRoleFromCreation(this RoleCreationRequest createRoleRequest)
    {
        var permissions = createRoleRequest
                            .Permissions
                            .Select(permissionId => new RolePermission(permissionId))
                            .ToList();

        return new(createRoleRequest.Name, permissions);
    }

    public static void ToRoleFromUpdate(this RoleUpdateRequest roleUpdateRequest, Role role)
    {
        var permissions = roleUpdateRequest
                            .Permissions
                            .Select(permissionId => new RolePermission(permissionId))
                            .ToList();

        role.Permissions = permissions;
        role.Name = roleUpdateRequest.Name;
    }
}