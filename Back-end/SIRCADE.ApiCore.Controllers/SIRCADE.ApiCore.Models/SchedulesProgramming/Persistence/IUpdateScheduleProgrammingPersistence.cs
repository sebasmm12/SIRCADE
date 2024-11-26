using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

public interface IUpdateScheduleProgrammingPersistence
{
    Task ExecuteAsync();

    Task ExecuteAsync(IEnumerable<int> scheduleProgrammingIds, ScheduleProgrammingState state);
}