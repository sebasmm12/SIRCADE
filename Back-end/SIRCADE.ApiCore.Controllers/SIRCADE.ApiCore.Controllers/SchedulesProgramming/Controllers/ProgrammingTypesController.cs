using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Controllers;

[ApiController]
[Route("api/programming/types")]
public class ProgrammingTypesController(IProgrammingTypesService programmingTypesService) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync()
    {
        var programmingTypes = await programmingTypesService.GetAsync();

        return Ok(programmingTypes);
    }
}