using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Permissions.Services;

namespace SIRCADE.ApiCore.Controllers.Permissions.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PermissionsController(IPermissionsService permissionsService) : ControllerBase
{

    [HttpGet("all")]
    public async Task<IActionResult> GetAsync()
    {
        var permissions = await permissionsService.GetAsync();

        return Ok(permissions);
    }

    [HttpGet("roles/{roleId:int}")]
    public async Task<IActionResult> GetAsync([FromRoute]int roleId)
    {
        var permissions = await permissionsService.GetAsync(roleId);

        return Ok(permissions);
    }
}