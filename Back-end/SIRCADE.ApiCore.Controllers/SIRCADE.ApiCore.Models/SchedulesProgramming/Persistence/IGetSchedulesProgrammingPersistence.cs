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
}