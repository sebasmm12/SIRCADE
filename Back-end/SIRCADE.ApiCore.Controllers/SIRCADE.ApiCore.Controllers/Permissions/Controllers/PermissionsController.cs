using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Permissions.Services;

namespace SIRCADE.ApiCore.Controllers.Permissions.Controllers;

[ApiController]
[Route("api/[controller]")]
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