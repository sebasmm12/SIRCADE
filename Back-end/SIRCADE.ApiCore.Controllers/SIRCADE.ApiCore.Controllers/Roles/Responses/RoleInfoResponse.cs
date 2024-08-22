using SIRCADE.ApiCore.Models.RolePermissions.DTOs;

namespace SIRCADE.ApiCore.Controllers.Roles.Responses;

public record RoleInfoResponse(int Id, string Name, IEnumerable<RolePermissionDto> Permissions);