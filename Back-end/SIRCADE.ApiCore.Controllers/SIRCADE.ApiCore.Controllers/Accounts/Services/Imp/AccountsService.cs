using SIRCADE.ApiCore.Controllers.Accounts.Requests;
using SIRCADE.ApiCore.Controllers.Accounts.Responses;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.ApiCore.Controllers.Accounts.Services.Imp;

public class AccountsService
    (IGetUsersPersistence getUsersPersistence, 
     IHashService hashService,
     IBearerTokenService bearerTokenService): IAccountsService
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
    
}