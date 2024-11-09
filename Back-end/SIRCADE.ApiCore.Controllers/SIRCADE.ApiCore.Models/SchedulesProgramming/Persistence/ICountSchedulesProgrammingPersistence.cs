using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

public interface ICountSchedulesProgrammingPersistence
{
    Task<int> ExecuteAsync(int clientId, DateTime startDate, int? scheduleProgrammingId);
    
    Task<int> ExecuteAsync(OverlappedScheduleProgrammingFiltersDto overlappedScheduleProgrammingFiltersDto);
}