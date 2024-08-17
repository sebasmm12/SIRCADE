using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.SportFields.Services;

namespace SIRCADE.ApiCore.Controllers.SportFields.Controllers;

[ApiController]
[Route("api/sport-fields/types")]
public class SportFieldTypesController(ISportFieldTypesService sportFieldTypesService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var sportFieldTypes = await sportFieldTypesService.GetSportFieldTypesAsync();

        return Ok(sportFieldTypes);
    }
}