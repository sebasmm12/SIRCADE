using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Accounts.Requests;
using SIRCADE.ApiCore.Controllers.Accounts.Services;

namespace SIRCADE.ApiCore.Controllers.Accounts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

    [HttpPut("passwords")]
    public async Task<IActionResult> UpdatePasswordAsync([FromBody] PasswordUpdateRequest passwordUpdateRequest)
    {
        try
        {
            await accountsService.UpdatePasswordAsync(passwordUpdateRequest);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}