using SIRCADE.ApiCore.Controllers.Reports.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.DTOs;

namespace SIRCADE.ApiCore.Controllers.Reports.Services;

public interface IReportsService
{
    Task<DataTableDto<ReportInfoResponse>> GetFrequentlyUsersAsync(FrequentlyUserDataTableQueriesDto frequentlyUserDataTableQueriesDto, bool isPaginated = true);
    Task<DataTableDto<ReportInfoResponse>> GetReservationsMonthlyAsync();
    Task<DataTableDto<ReportInfoResponse>> GetReservationsYearlyAsync();
    Task<DataTableDto<ReportInfoResponse>> GetReservationsDailyAsync();
    Task<DataTableDto<ReportInfoResponse>> GetReservationsWeeklyAsync();
    Task<string> ExportFrequentlyUsersAsync(FrequentlyUserExportQueriesDto frequentlyUserExportQueriesDto);
    Task<string> ExportReservationsAsync(Func<Task<DataTableDto<ReportInfoResponse>>> reservationsAsyncFunc, string reportTitle);
}