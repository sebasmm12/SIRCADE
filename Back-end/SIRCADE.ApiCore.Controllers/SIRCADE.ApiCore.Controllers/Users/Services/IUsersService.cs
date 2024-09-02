using SIRCADE.ApiCore.Controllers.Users.Requests;
using SIRCADE.ApiCore.Controllers.Users.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.DTOs;

namespace SIRCADE.ApiCore.Controllers.Users.Services;

public interface IUsersService
{
    Task<int> CreateAsync(UserCreationRequest userCreationRequest);

    Task<DataTableDto<UserResponse>> GetAsync(UserDataTableQueriesDto userDataTableQueries);

    Task UpdateActiveAsync(int userId);

    Task<UserInfoResponse> GetAsync(int userId);
    Task UpdateAsync(UserUpdateRequest userUpdateRequest);
}