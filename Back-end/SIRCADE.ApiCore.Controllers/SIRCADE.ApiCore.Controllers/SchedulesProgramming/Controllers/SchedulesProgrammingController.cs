using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Controllers;

[ApiController]
[Route("api/schedules-programming")]
public class SchedulesProgrammingController(ISchedulesProgrammingService schedulesProgrammingService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ScheduleProgrammingRegisterRequest scheduleProgrammingCreationRequest)
    {
        var scheduleProgrammingId = await schedulesProgrammingService.CreateAsync(scheduleProgrammingCreationRequest);

        return Ok(scheduleProgrammingId);
    }
}