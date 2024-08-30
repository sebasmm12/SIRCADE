using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services;

public interface ISchedulesProgrammingService
{
    Task<int> CreateAsync(ScheduleProgrammingRegisterRequest scheduleProgrammingRegisterRequest);
}