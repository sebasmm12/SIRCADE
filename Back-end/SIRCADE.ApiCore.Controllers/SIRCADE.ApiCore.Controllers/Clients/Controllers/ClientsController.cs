using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Clients.Services;

namespace SIRCADE.ApiCore.Controllers.Clients.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ClientsController(IClientsService clientsService) : ControllerBase
{

    [HttpGet("all")]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var clients = await clientsService.GetAllAsync();

            return Ok(clients);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}