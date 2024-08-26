using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Mappers;

public static class ProgrammingTypesMapper
{
    public static ProgrammingTypeInfoResponse MapToProgrammingTypeInfoResponse(this ProgrammingType programmingType)
    {
        return new(programmingType.Id, programmingType.Name);
    }
}