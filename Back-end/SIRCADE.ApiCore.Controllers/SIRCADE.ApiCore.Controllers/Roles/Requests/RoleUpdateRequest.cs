namespace SIRCADE.ApiCore.Controllers.Roles.Requests;

public record RoleUpdateRequest(int Id, string Name, IEnumerable<int> Permissions)
{
}