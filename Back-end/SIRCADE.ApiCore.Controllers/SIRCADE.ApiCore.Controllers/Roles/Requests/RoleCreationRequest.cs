namespace SIRCADE.ApiCore.Controllers.Roles.Requests;

public record RoleCreationRequest(string Name, IEnumerable<int> Permissions)
{
}