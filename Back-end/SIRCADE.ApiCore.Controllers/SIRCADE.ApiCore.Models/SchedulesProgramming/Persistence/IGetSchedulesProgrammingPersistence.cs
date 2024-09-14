using SIRCADE.ApiCore.Models.Dashboards.DTOs;
using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Queries;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

public interface IGetSchedulesProgrammingPersistence
{
    Task<IEnumerable<ScheduleProgramming>> ExecuteAsync(SchedulesProgrammingWeeklyQueries schedulesProgrammingWeeklyQueries);

    Task<ScheduleProgramming> ExecuteAsync(int scheduleProgrammingId, bool needsInclude = false,
        bool isTracked = false);

    Task<ScheduleProgramming?> ExecuteAsync(ScheduleProgrammingFiltersDto scheduleProgrammingFiltersDto);

    Task<IEnumerable<ScheduleProgramming>> ExecuteAsync(DashboardFiltersDto dashboardFilters);
    Task<IEnumerable<ScheduleProgramming>> ExecuteAsync(DashboardTimeType dashboardTimeType, object? filters = null, bool canIgnoreQueryFilters = true);
}