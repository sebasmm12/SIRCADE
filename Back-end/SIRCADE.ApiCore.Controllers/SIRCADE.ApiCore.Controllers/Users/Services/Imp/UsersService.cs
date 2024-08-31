using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Controllers.Users.Mappers;
using SIRCADE.ApiCore.Controllers.Users.Requests;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.Entities;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.ApiCore.Controllers.Users.Services.Imp;

public class UsersService
    (IHashService hashService,
     ICreateUserPersistence createUserPersistence): IUsersService
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

    #region private methods

    private static void SetPasswordToUser(HashDto hashDto, User user)
    {
        user.Password = hashDto.Hash;
        user.Salt = hashDto.Salt;
    }


    #endregion
}