using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Dashboards.Services;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;

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

    [HttpGet("reservations")]
    public async Task<IActionResult> GetCancelledReservations([FromQuery] ScheduleProgrammingState? reservationState)
    {
        try
        {
            var cancelledReservations = await dashboardService.GetReservationsAsync(reservationState);

            return Ok(cancelledReservations);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("reservations/grades")]
    public async Task<IActionResult> GetReservationsByGradeAsync()
    {
        try
        {
            var reservationsByGrade = await dashboardService.GetReservationsByGradeAsync();

            return Ok(reservationsByGrade);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("reservations/months")]
    public async Task<IActionResult> GetReservationsByYearAsync()
    {
        try
        {
            var reservationsByYear = await dashboardService.GetReservationsByMonthsAsync();

            return Ok(reservationsByYear);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}