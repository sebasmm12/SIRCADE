using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Queries;

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

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] SchedulesProgrammingWeeklyQueries schedulesProgrammingWeeklyQueries) 
    {
        var schedulesProgramming = await schedulesProgrammingService.GetAsync(schedulesProgrammingWeeklyQueries);

        return Ok(schedulesProgramming);
    }
}