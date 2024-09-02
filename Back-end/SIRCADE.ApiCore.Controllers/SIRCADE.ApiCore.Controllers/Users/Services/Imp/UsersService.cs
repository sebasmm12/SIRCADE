using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Controllers.Users.Mappers;
using SIRCADE.ApiCore.Controllers.Users.Requests;
using SIRCADE.ApiCore.Controllers.Users.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.DTOs;
using SIRCADE.ApiCore.Models.Users.Entities;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.ApiCore.Controllers.Users.Services.Imp;

public class UsersService
    (IHashService hashService,
     ICreateUserPersistence createUserPersistence,
     IGetUsersPersistence getUsersPersistence,
     IUpdateUsersPersistence updateUsersPersistence): IUsersService
{
    public async Task<int> CreateAsync(UserCreationRequest userCreationRequest)
    {
        var user = userCreationRequest.MapToUser();

        // TODO: Set the default password in the app settings
        const string password = "Sircade123!";

        var generatedHash = hashService.Generate(password);

        SetPasswordToUser(generatedHash, user);

        await createUserPersistence.ExecuteAsync(user);

        return user.Id;
    }

    public async Task<DataTableDto<UserResponse>> GetAsync(UserDataTableQueriesDto userDataTableQueries)
    {
        var users = await getUsersPersistence.ExecuteAsync(userDataTableQueries);

        var usersResponse = users.Data.Select(user => user.MapToUserResponse());

        return new(usersResponse, users.TotalElements);
    }

    public async Task<UserInfoResponse> GetAsync(int userId)
    {
        var user = await getUsersPersistence.ExecuteAsync(userId);

        var userResponse = user.MapToUserInfoResponse();

        return userResponse;
    }

    public async Task UpdateActiveAsync(int userId)
    {
        var user = await getUsersPersistence.ExecuteAsync(userId);

        user.Detail.Associated = !user.Detail.Associated;

        await updateUsersPersistence.ExecuteAsync(user);
    }

    public async Task UpdateAsync(UserUpdateRequest userUpdateRequest)
    {
        var user = await getUsersPersistence.ExecuteAsync(userUpdateRequest.Id);

        userUpdateRequest.MapToUser(user);

        await updateUsersPersistence.ExecuteAsync(user);
    }

    #region private methods

    private static void SetPasswordToUser(HashDto hashDto, User user)
    {
        user.Password = hashDto.Hash;
        user.Salt = hashDto.Salt;
    }


    #endregion
}