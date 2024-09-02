using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Users.Services;

namespace SIRCADE.ApiCore.Controllers.Users.Controllers;

[ApiController]
[Route("api/users/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UnitiesController(IUnitiesService unitiesService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var unities = await unitiesService.GetAllAsync();

        return Ok(unities);
    }
}