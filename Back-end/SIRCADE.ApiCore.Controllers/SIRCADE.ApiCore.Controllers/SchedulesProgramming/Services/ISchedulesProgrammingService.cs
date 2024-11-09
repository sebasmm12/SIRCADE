using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Queries;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services;

public interface ISchedulesProgrammingService
{
    Task<int> CreateAsync(ScheduleProgrammingRegisterRequest scheduleProgrammingRegisterRequest);

    Task<IEnumerable<ScheduleProgrammingInfoResponse>> GetAsync(SchedulesProgrammingWeeklyQueries schedulesProgrammingWeeklyQueries);
    
    Task UpdateAsync(ScheduleProgrammingUpdateRequest scheduleProgrammingUpdateRequest);
    
    Task<ScheduleProgrammingInfoResponse> GetAsync(int scheduleProgrammingId);
    
    Task CancelAsync(int scheduleProgrammingId);
    
    Task<int> GetOverlappedAsync(OverlappedScheduleProgrammingFiltersDto overlappedScheduleProgrammingFiltersDto);
}