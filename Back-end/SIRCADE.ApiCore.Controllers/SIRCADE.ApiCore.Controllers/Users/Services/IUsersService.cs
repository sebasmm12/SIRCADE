using SIRCADE.ApiCore.Controllers.Users.Requests;

namespace SIRCADE.ApiCore.Controllers.Users.Services;

public interface IUsersService
{
    Task<int> CreateAsync(UserCreationRequest userCreationRequest);
}