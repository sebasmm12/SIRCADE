using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Controllers.Users.Mappers;
using SIRCADE.ApiCore.Controllers.Users.Requests;
using SIRCADE.ApiCore.Controllers.Users.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.DTOs;
using SIRCADE.ApiCore.Models.Users.Entities;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.ApiCore.Controllers.Users.Services.Imp;

public class UsersService(
    IHashService hashService,
    ICreateUserPersistence createUserPersistence,
    IGetUsersPersistence getUsersPersistence,
    IUpdateUsersPersistence updateUsersPersistence,
    IExistUsersPersistence existUsersPersistence,
    IConfiguration configuration) : IUsersService
{
    public async Task<int> CreateAsync(UserCreationRequest userCreationRequest)
    {
        await ProcessValidationAsync(userCreationRequest);

        var user = userCreationRequest.MapToUser();

        var password = configuration.GetValue<string>("DefaultPassword")!;

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

    public async Task<bool> ValidateNsaAsync(int nsa)
    {
        var exists = await existUsersPersistence.ExecuteAsync(nsa.ToString());

        return !exists;
    }

    #region private methods

    private static void SetPasswordToUser(HashDto hashDto, User user)
    {
        user.Password = hashDto.Hash;
        user.Salt = hashDto.Salt;
    }

    private async Task ProcessValidationAsync(UserCreationRequest request)
    {
        var validationMessage = await ValidateRequestAsync(request);

        if (validationMessage.IsValid)
           return;

        throw new(validationMessage.Message);
    }

    private async Task<ValidateMessageDto<User>> ValidateRequestAsync(UserCreationRequest request)
    {
        var exists  = await existUsersPersistence.ExecuteAsync(request.Nsa, request.DocumentNumber);

        if (exists)
            return new(false, "Ya existe un usuario con el mismo NSA o DNI");

        return new(true, string.Empty);
    }


    #endregion
}