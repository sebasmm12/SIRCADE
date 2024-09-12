using SIRCADE.ApiCore.Controllers.Reports.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.DTOs;

namespace SIRCADE.ApiCore.Controllers.Reports.Services;

public interface IReportsService
{
    Task<DataTableDto<ReportInfoResponse>> GetFrequentlyUsersAsync(FrequentlyUserDataTableQueriesDto frequentlyUserDataTableQueriesDto);
    Task<DataTableDto<ReportInfoResponse>> GetReservationsMonthlyAsync();
    Task<DataTableDto<ReportInfoResponse>> GetReservationsYearlyAsync();
    Task<DataTableDto<ReportInfoResponse>> GetReservationsDailyAsync();
    Task<DataTableDto<ReportInfoResponse>> GetReservationsWeeklyAsync();
}