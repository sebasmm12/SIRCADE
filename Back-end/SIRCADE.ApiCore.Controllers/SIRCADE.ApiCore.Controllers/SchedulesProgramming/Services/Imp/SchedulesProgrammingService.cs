using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Mappers;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services.Imp;

public class SchedulesProgrammingService
    (ICreateScheduleProgrammingPersistence createScheduleProgrammingPersistence): ISchedulesProgrammingService
{

    public async Task<int> CreateAsync(ScheduleProgrammingRegisterRequest scheduleProgrammingRegisterRequest)
    {
        // TODO: Get the current user through the authentication bearer token service

        var scheduleProgramming = scheduleProgrammingRegisterRequest.MapToScheduleProgramming(1);

        return await createScheduleProgrammingPersistence.ExecuteAsync(scheduleProgramming);
    }
}