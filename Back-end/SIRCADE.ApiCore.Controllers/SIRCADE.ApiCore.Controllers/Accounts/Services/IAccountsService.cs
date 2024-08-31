using SIRCADE.ApiCore.Controllers.Accounts.Requests;
using SIRCADE.ApiCore.Controllers.Accounts.Responses;

namespace SIRCADE.ApiCore.Controllers.Accounts.Services;

public interface IAccountsService
{
    Task<AccountInfoResponse> GenerateTokenAsync(AccountCredentialsRequest accountCredentialsRequest);
}