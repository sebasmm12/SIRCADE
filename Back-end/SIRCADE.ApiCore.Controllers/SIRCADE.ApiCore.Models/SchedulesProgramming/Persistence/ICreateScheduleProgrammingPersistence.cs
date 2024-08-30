using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

public interface ICreateScheduleProgrammingPersistence
{
    Task<int> ExecuteAsync(ScheduleProgramming scheduleProgramming);
}