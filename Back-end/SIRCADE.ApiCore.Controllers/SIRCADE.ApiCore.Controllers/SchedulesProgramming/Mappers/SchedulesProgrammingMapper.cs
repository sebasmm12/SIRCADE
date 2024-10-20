using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
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
                   scheduleProgramming.GetClientFullName(),
                   scheduleProgramming.StartDate,
                   scheduleProgramming.EndDate,
                   scheduleProgramming.Comment,
                   scheduleProgramming.GetRegisterUserFullName(),
                   scheduleProgramming.Type,
                   scheduleProgramming.ProgrammingType.Name,
                   scheduleProgramming.ProgrammingType.LightColor,
                   scheduleProgramming.ProgrammingType.DarkColor);
    }

    public static ScheduleProgramming MapToScheduleProgramming(
        this ScheduleProgrammingUpdateRequest scheduleProgrammingUpdateRequest, ScheduleProgramming scheduleProgramming)
    {
        scheduleProgramming.StartDate = scheduleProgrammingUpdateRequest.StartDate;
        scheduleProgramming.EndDate = scheduleProgrammingUpdateRequest.EndDate;
        scheduleProgramming.State = ScheduleProgrammingState.ReScheduled;

        return scheduleProgramming;
    }


    public static ScheduleProgrammingFiltersDto MapToScheduleFiltersDto(
        this ScheduleProgrammingRegisterRequest scheduleProgrammingRegisterRequest, string restrictedType)
    {
        return new(scheduleProgrammingRegisterRequest.SportFieldId,
                   scheduleProgrammingRegisterRequest.StartDate,
                   scheduleProgrammingRegisterRequest.EndDate,
                   scheduleProgrammingRegisterRequest.Type,
                   restrictedType);
    }

    public static ScheduleProgrammingFiltersDto MapToScheduleFiltersDto(
        this ScheduleProgramming scheduleProgramming, string restrictedType)
    {
        return new(
                   scheduleProgramming.SportFieldId,
                   scheduleProgramming.StartDate,
                   scheduleProgramming.EndDate,
                   scheduleProgramming.Type,
                   restrictedType,
                   scheduleProgramming.Id);
    }
}