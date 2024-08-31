using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Mappers;

public static class SchedulesProgrammingMapper
{
    public static ScheduleProgramming MapToScheduleProgramming(
        this ScheduleProgrammingRegisterRequest scheduleProgrammingRegisterRequest, int registerUserId)
    {
        return new(scheduleProgrammingRegisterRequest.SportFieldId,
                   scheduleProgrammingRegisterRequest.ClientId,
                   scheduleProgrammingRegisterRequest.StartDate,
                   scheduleProgrammingRegisterRequest.EndDate,
                   ScheduleProgrammingState.Reserved,
                   scheduleProgrammingRegisterRequest.Comment,
                   scheduleProgrammingRegisterRequest.Type,
                   registerUserId,
                   DateTime.Now);
    }


    public static ScheduleProgrammingInfoResponse MapToScheduleProgrammingInfoResponse(
        this ScheduleProgramming scheduleProgramming)
    {
        return new(scheduleProgramming.Id,
                   scheduleProgramming.SportFieldId,
                   scheduleProgramming.SportField.Name,
                   scheduleProgramming.ClientId,
                   scheduleProgramming.Client?.Detail.Names,
                   scheduleProgramming.StartDate,
                   scheduleProgramming.EndDate,
                   scheduleProgramming.Comment,
                   scheduleProgramming.Type,
                   scheduleProgramming.ProgrammingType.Name);
    }
}