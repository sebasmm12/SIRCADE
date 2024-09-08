using SIRCADE.ApiCore.Controllers.Dashboards.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;

namespace SIRCADE.ApiCore.Controllers.Dashboards.Services;

public interface IDashboardsService
{
    Task<DashboardWidgetsResponse> GetWidgetsAsync();

    Task<IEnumerable<DashboardResponse>> GetReservationsAsync(ScheduleProgrammingState? state);
    Task<IEnumerable<DashboardResponse>> GetReservationsByGradeAsync();
    Task<IEnumerable<DashboardResponse>> GetReservationsByMonthsAsync();
}