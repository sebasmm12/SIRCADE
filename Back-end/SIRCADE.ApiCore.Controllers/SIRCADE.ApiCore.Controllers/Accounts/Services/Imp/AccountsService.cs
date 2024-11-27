using System.Security.Claims;
using SIRCADE.ApiCore.Controllers.Accounts.Requests;
using SIRCADE.ApiCore.Controllers.Accounts.Responses;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.Entities;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.ApiCore.Controllers.Accounts.Services.Imp;

public class AccountsService
    (IGetUsersPersistence getUsersPersistence, 
     IHashService hashService,
     IBearerTokenService bearerTokenService,
     IHttpContextAccessor httpContextAccessor,
     IUpdateUsersPersistence updateUsersPersistence): IAccountsService
{

    public async Task<AccountInfoResponse> GenerateTokenAsync(AccountCredentialsRequest accountCredentialsRequest)
    {
        var user = await getUsersPersistence.ExecuteAsync(accountCredentialsRequest.Nsa);

        var isUserPassword = hashService.Compare(accountCredentialsRequest.Password, user.Password, user.Salt);

        if(!isUserPassword)
            throw new("Credenciales inválidas");

        var accountInfoResponse = bearerTokenService.Generate(user);

        return accountInfoResponse;
    }

    public async Task UpdatePasswordAsync(PasswordUpdateRequest passwordUpdateRequest)
    {
        var user = await ValidateCurrentPasswordAsync(passwordUpdateRequest.CurrentPassword);

        var newPasswordHash = hashService.Generate(passwordUpdateRequest.NewPassword);

        SetPasswordToUser(newPasswordHash, user);

        await updateUsersPersistence.ExecuteAsync(user);
    }

    #region private methods
    private static void SetPasswordToUser(HashDto hashDto, User user)
    {
        user.Password = hashDto.Hash;
        user.Salt = hashDto.Salt;
    }

    private async Task<User> ValidateCurrentPasswordAsync(string currentPassword)
    {
        var userId = int.Parse(
            httpContextAccessor.HttpContext!
                .User
                .Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)!
                .Value);

        var user = await getUsersPersistence.ExecuteAsync(userId);

        var isUserPassword = hashService.Compare(currentPassword, user.Password, user.Salt);

        if (!isUserPassword)
            throw new("La contraseña actual no es la correcta");

        return user;
    }
    #endregion

}