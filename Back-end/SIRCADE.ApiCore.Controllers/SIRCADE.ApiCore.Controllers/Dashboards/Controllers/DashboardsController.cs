using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Dashboards.Services;

namespace SIRCADE.ApiCore.Controllers.Dashboards.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DashboardsController(IDashboardsService dashboardService) : ControllerBase
{
    [HttpGet("widgets")]
    public async Task<IActionResult> GetWidgetsAsync()
    {
        try
        {
            var widgets = await dashboardService.GetWidgetsAsync();

            return Ok(widgets);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        
    }
}