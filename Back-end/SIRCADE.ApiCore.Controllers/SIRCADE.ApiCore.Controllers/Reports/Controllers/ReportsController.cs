using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Reports.Services;
using SIRCADE.ApiCore.Models.Users.DTOs;

namespace SIRCADE.ApiCore.Controllers.Reports.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController (IReportsService reportsService): ControllerBase
{
    [HttpGet("frequently-users")]
    public async Task<IActionResult> GetFrequentlyUsersAsync([FromQuery] FrequentlyUserDataTableQueriesDto frequentlyUserDataTableQueriesDto)
    {
        var response = await reportsService.GetFrequentlyUsersAsync(frequentlyUserDataTableQueriesDto);

        return Ok(response);
    }

    [HttpGet("monthly-reservations")]
    public async Task<IActionResult> GetReservationsMonthlyAsync()
    {
        var response = await reportsService.GetReservationsMonthlyAsync();

        return Ok(response);
    }

    [HttpGet("yearly-reservations")]
    public async Task<IActionResult> GetReservationsYearlyAsync()
    {
        var response = await reportsService.GetReservationsYearlyAsync();

        return Ok(response);
    }

    [HttpGet("daily-reservations")]
    public async Task<IActionResult> GetReservationsDailyAsync()
    {
        var response = await reportsService.GetReservationsDailyAsync();

        return Ok(response);
    }

    [HttpGet("weekly-reservations")]
    public async Task<IActionResult> GetReservationsWeeklyAsync()
    {
        var response = await reportsService.GetReservationsWeeklyAsync();

        return Ok(response);
    }
}