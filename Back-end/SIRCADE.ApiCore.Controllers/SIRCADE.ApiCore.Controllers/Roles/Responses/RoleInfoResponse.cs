namespace SIRCADE.ApiCore.Controllers.Roles.Responses;

public record RoleInfoResponse(int Id, string Name, IEnumerable<int> Permissions);