using SIRCADE.ApiCore.Controllers.Roles.Mappers;
using SIRCADE.ApiCore.Controllers.Roles.Requests;
using SIRCADE.ApiCore.Controllers.Roles.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Queries;
using SIRCADE.ApiCore.Models.Roles.Persistence;

namespace SIRCADE.ApiCore.Controllers.Roles.Services.Imp;

public class RolesService(
    IGetRolesPersistence getRolesPersistence, 
    ICreateRolePersistence createRolePersistence,
    IUpdateRolePersistence updateRolePersistence,
    IExistRolePersistence existRolePersistence,
    IDeleteRolePersistence deleteRolePersistence) : IRolesService
{

    public async Task<IEnumerable<RoleResponse>> GetAsync()
    {
        var roles = await getRolesPersistence.ExecuteAsync();

        var rolesResponse = roles.Select(role => role.ToRoleResponse());

        return rolesResponse;
    }

    public async Task<RoleInfoResponse> GetAsync(int roleId)
    {
        var role = await getRolesPersistence.ExecuteAsync(roleId, needsInclude: true);

        var roleResponse = role.ToRoleInfoResponse();

        return roleResponse;
    }

    public async Task<DataTableDto<RoleResponse>> GetAsync(DataTableQueriesDto dataTableQueries)
    {
        var roles = await getRolesPersistence.ExecuteAsync(dataTableQueries);

        var rolesResponse = roles.Data.Select(role => role.ToRoleResponse());

        return new(rolesResponse, roles.TotalElements);
    }

    public async Task<int> CreateAsync(RoleCreationRequest roleCreationRequest)
    {
        var role = roleCreationRequest.ToRoleFromCreation();

        await createRolePersistence.ExecuteAsync(role);

        return role.Id;
    }

    public async Task UpdateAsync(RoleUpdateRequest roleUpdateRequest)
    {
        var currentRole = await getRolesPersistence.ExecuteAsync(roleUpdateRequest.Id, true);

        roleUpdateRequest.ToRoleFromUpdate(currentRole);

        await updateRolePersistence.ExecuteAsync();
    }

    public async Task UpdateStatusAsync(int roleId)
    {
        var currentRole = await getRolesPersistence.ExecuteAsync(roleId);

        currentRole.Active = !currentRole.Active;

        await updateRolePersistence.ExecuteAsync();
    }

    public async Task DeleteAsync(int roleId)
    {
        var existsRole = await existRolePersistence.ExecuteAsync(roleId);

        if(!existsRole)
            throw new ArgumentException($"Role with Id { roleId } not found");

        var role = await getRolesPersistence.ExecuteAsync(roleId, needsInclude: true, isTracked: true);

        await deleteRolePersistence.ExecuteAsync(role);
    }
}