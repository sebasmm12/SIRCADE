using SIRCADE.ApiCore.Controllers.Reports.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.DTOs;

namespace SIRCADE.ApiCore.Controllers.Reports.Services;

public interface IReportsService
{
    Task<DataTableDto<FrequentlyUserByReservationResponse>> GetFrequentlyUsersAsync(FrequentlyUserDataTableQueriesDto frequentlyUserDataTableQueriesDto);
    Task<DataTableDto<ReservationInTimeResponse>> GetReservationsMonthlyAsync();
    Task<DataTableDto<ReservationInTimeResponse>> GetReservationsYearlyAsync();
    Task<DataTableDto<ReservationInTimeResponse>> GetReservationsDailyAsync();
    Task<DataTableDto<ReservationInTimeResponse>> GetReservationsWeeklyAsync();
}