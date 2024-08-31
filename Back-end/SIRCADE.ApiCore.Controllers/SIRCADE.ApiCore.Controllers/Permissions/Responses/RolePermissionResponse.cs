using SIRCADE.ApiCore.Models.Permissions.Enums;

namespace SIRCADE.ApiCore.Controllers.Permissions.Responses;

public record RolePermissionResponse(
    int Id, 
    string Name, 
    PermissionType Type, 
    string? Url, 
    string? Icon);