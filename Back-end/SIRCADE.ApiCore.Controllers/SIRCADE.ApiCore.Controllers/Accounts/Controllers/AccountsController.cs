using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Accounts.Requests;
using SIRCADE.ApiCore.Controllers.Accounts.Services;

namespace SIRCADE.ApiCore.Controllers.Accounts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(IAccountsService accountsService) : ControllerBase
{

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateTokenAsync(AccountCredentialsRequest accountCredentialsRequest)
    {
        try
        {
            var accountInfo = await accountsService.GenerateTokenAsync(accountCredentialsRequest);

            return Ok(accountInfo);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }
}