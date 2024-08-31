using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Mappers;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Queries;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services.Imp;

public class SchedulesProgrammingService
    (ICreateScheduleProgrammingPersistence createScheduleProgrammingPersistence, 
     IGetSchedulesProgrammingPersistence getSchedulesProgrammingPersistence): ISchedulesProgrammingService
{

    public async Task<int> CreateAsync(ScheduleProgrammingRegisterRequest scheduleProgrammingRegisterRequest)
    {
        // TODO: Get the current user through the authentication bearer token service

        var scheduleProgramming = scheduleProgrammingRegisterRequest.MapToScheduleProgramming(1);

        return await createScheduleProgrammingPersistence.ExecuteAsync(scheduleProgramming);
    }

    public async Task<IEnumerable<ScheduleProgrammingInfoResponse>> GetAsync(SchedulesProgrammingWeeklyQueries schedulesProgrammingWeeklyQueries)
    {
        var schedulesProgramming = await getSchedulesProgrammingPersistence.ExecuteAsync(schedulesProgrammingWeeklyQueries);

        var schedulesProgrammingResponse = schedulesProgramming.Select(scheduleProgramming => scheduleProgramming.MapToScheduleProgrammingInfoResponse());

        return schedulesProgrammingResponse;
    }
}