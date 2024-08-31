using SIRCADE.ApiCore.Controllers.Accounts.Responses;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;

public interface IBearerTokenService
{
    AccountInfoResponse Generate(User user);
}