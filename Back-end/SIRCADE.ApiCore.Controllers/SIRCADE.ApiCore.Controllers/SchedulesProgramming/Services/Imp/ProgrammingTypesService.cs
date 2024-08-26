using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Mappers;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services.Imp;

public class ProgrammingTypesService
    (IGetProgrammingTypesPersistence getProgrammingTypesPersistence): IProgrammingTypesService
{
    public async Task<IEnumerable<ProgrammingTypeInfoResponse>> GetAsync()
    {
        var programmingTypes = await getProgrammingTypesPersistence.ExecuteAsync();

        var programmingTypesResponse = programmingTypes.Select(programmingType => programmingType.MapToProgrammingTypeInfoResponse());

        return programmingTypesResponse;
    }
}