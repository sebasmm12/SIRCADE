using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services;

public interface IProgrammingTypesService
{
    Task<IEnumerable<ProgrammingTypeInfoResponse>> GetAsync();
}