using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Queries;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Controllers;

[ApiController]
[Route("api/schedules-programming")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SchedulesProgrammingController(ISchedulesProgrammingService schedulesProgrammingService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] SchedulesProgrammingWeeklyQueries schedulesProgrammingWeeklyQueries) 
    {
        try
        {
            var schedulesProgramming = await schedulesProgrammingService.GetAsync(schedulesProgrammingWeeklyQueries);

            return Ok(schedulesProgramming);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet("{scheduleProgrammingId:int}")]
    public async Task<IActionResult> GetAsync(int scheduleProgrammingId)
    {
        try
        {
            var scheduleProgramming = await schedulesProgrammingService.GetAsync(scheduleProgrammingId);

            return Ok(scheduleProgramming);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("total-overlapped")]
    public async Task<IActionResult> GetTotalOverlapped(
        [FromQuery] OverlappedScheduleProgrammingFiltersDto overlappedScheduleProgrammingFiltersDto)
    {
        try
        {
            var totalSchedulesProgramming = await schedulesProgrammingService.GetOverlappedAsync(overlappedScheduleProgrammingFiltersDto);

            return Ok(totalSchedulesProgramming);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ScheduleProgrammingRegisterRequest scheduleProgrammingCreationRequest)
    {
        try
        {
            var scheduleProgrammingId = await schedulesProgrammingService.CreateAsync(scheduleProgrammingCreationRequest);

            return Ok(scheduleProgrammingId);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] ScheduleProgrammingUpdateRequest scheduleProgrammingUpdateRequest)
    {
        try
        {
            await schedulesProgrammingService.UpdateAsync(scheduleProgrammingUpdateRequest);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{scheduleProgrammingId:int}")]
    public async Task<IActionResult> CancelAsync(int scheduleProgrammingId)
    {
        try
        {
            await schedulesProgrammingService.CancelAsync(scheduleProgrammingId);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}