using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Reports.Services;
using SIRCADE.ApiCore.Models.Users.DTOs;

namespace SIRCADE.ApiCore.Controllers.Reports.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController(IReportsService reportsService) : ControllerBase
{
    [HttpGet("frequently-users")]
    public async Task<IActionResult> GetFrequentlyUsersAsync(
        [FromQuery] FrequentlyUserDataTableQueriesDto frequentlyUserDataTableQueriesDto)
    {
        try
        {
            var response = await reportsService.GetFrequentlyUsersAsync(frequentlyUserDataTableQueriesDto);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpGet("frequently-users/exports")]
    public async Task<IActionResult> ExportFrequentlyUsersAsync(
        [FromQuery] FrequentlyUserExportQueriesDto frequentlyUserExportQueriesDto)
    {
        try
        {
            var response = await reportsService.ExportFrequentlyUsersAsync(frequentlyUserExportQueriesDto);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpGet("monthly-reservations")]
    public async Task<IActionResult> GetReservationsMonthlyAsync()
    {
        try
        {
            var response = await reportsService.GetReservationsMonthlyAsync();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpGet("monthly-reservations/exports")]
    public async Task<IActionResult> ExportReservationsMonthlyAsync([FromQuery] string reportTitle)
    {
        try
        {
            var response =
                await reportsService.ExportReservationsAsync(reportsService.GetReservationsMonthlyAsync, reportTitle);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("yearly-reservations")]
    public async Task<IActionResult> GetReservationsYearlyAsync()
    {
        try
        {
            var response = await reportsService.GetReservationsYearlyAsync();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpGet("yearly-reservations/exports")]
    public async Task<IActionResult> ExportReservationsYearlyAsync([FromQuery] string reportTitle)
    {
        try
        {
            var response =
                await reportsService.ExportReservationsAsync(reportsService.GetReservationsYearlyAsync, reportTitle);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("daily-reservations")]
    public async Task<IActionResult> GetReservationsDailyAsync()
    {
        try
        {
            var response = await reportsService.GetReservationsDailyAsync();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpGet("daily-reservations/exports")]
    public async Task<IActionResult> ExportReservationsDailyAsync([FromQuery] string reportTitle)
    {
        try
        {
            var response =
                await reportsService.ExportReservationsAsync(reportsService.GetReservationsDailyAsync, reportTitle);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("weekly-reservations")]
    public async Task<IActionResult> GetReservationsWeeklyAsync()
    {
        try
        {
            var response = await reportsService.GetReservationsDailyAsync();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("weekly-reservations/exports")]
    public async Task<IActionResult> ExportReservationsWeeklyAsync([FromQuery] string reportTitle)
    {
        try
        {
            var response =
                await reportsService.ExportReservationsAsync(reportsService.GetReservationsWeeklyAsync, reportTitle);

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}