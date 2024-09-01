using System.Security.Claims;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Mappers;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Queries;

namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Services.Imp;

public class SchedulesProgrammingService
    (ICreateScheduleProgrammingPersistence createScheduleProgrammingPersistence, 
     IGetSchedulesProgrammingPersistence getSchedulesProgrammingPersistence,
     IUpdateScheduleProgrammingPersistence updateScheduleProgrammingPersistence,
     IGetProgrammingTypesPersistence getProgrammingTypesPersistence,
     IHttpContextAccessor httpContextAccessor): ISchedulesProgrammingService
{

    public async Task<int> CreateAsync(ScheduleProgrammingRegisterRequest scheduleProgrammingRegisterRequest)
    {
        await ProcessValidationAsync(scheduleProgrammingRegisterRequest.MapToScheduleFiltersDto("Reserva"));

        var userId = Convert.ToInt32(httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);

        var scheduleProgramming = scheduleProgrammingRegisterRequest.MapToScheduleProgramming(userId);

        return await createScheduleProgrammingPersistence.ExecuteAsync(scheduleProgramming);
    }

    public async Task<IEnumerable<ScheduleProgrammingInfoResponse>> GetAsync(SchedulesProgrammingWeeklyQueries schedulesProgrammingWeeklyQueries)
    {
        var schedulesProgramming = await getSchedulesProgrammingPersistence.ExecuteAsync(schedulesProgrammingWeeklyQueries);

        var schedulesProgrammingResponse = schedulesProgramming.Select(scheduleProgramming => scheduleProgramming.MapToScheduleProgrammingInfoResponse());

        return schedulesProgrammingResponse;
    }

    public async Task<ScheduleProgrammingInfoResponse> GetAsync(int scheduleProgrammingId)
    {
        var scheduleProgramming = await getSchedulesProgrammingPersistence.ExecuteAsync(scheduleProgrammingId, isTracked: false, needsInclude: true);

        return scheduleProgramming.MapToScheduleProgrammingInfoResponse();
    }

    public async Task UpdateAsync(ScheduleProgrammingUpdateRequest scheduleProgrammingUpdateRequest)
    {
        var scheduleProgramming = await getSchedulesProgrammingPersistence.ExecuteAsync(scheduleProgrammingUpdateRequest.Id, isTracked: true, needsInclude: true);

        scheduleProgrammingUpdateRequest.MapToScheduleProgramming(scheduleProgramming);

        await ProcessValidationAsync(scheduleProgramming.MapToScheduleFiltersDto(scheduleProgramming.ProgrammingType.Name));

        await updateScheduleProgrammingPersistence.ExecuteAsync();
    }

    public async Task CancelAsync(int scheduleProgrammingId)
    {
        var scheduleProgramming = await getSchedulesProgrammingPersistence.ExecuteAsync(scheduleProgrammingId, isTracked: true);

        await CancelAsync(scheduleProgramming);
    }

    #region private methods

    private async Task CancelAsync(ScheduleProgramming scheduleProgramming)
    {
        scheduleProgramming.State = ScheduleProgrammingState.Cancelled;

        await updateScheduleProgrammingPersistence.ExecuteAsync();
    }

    private async Task ProcessValidationAsync(ScheduleProgrammingFiltersDto programmingFiltersDto)
    {
        var validationMessage = await ValidateRequestAsync(programmingFiltersDto);

        if (!validationMessage.IsValid)
            throw new(validationMessage.Message);

        if (validationMessage.ScheduleProgramming is not null)
            await CancelAsync(validationMessage.ScheduleProgramming);
    }

    private async Task<ValidateMessageDto> ValidateRequestAsync(ScheduleProgrammingFiltersDto programmingFiltersDto)
    {
        var currentScheduleProgramming = await getSchedulesProgrammingPersistence.ExecuteAsync(programmingFiltersDto);

        if (currentScheduleProgramming is null)
            return new(true, string.Empty);

        var programmingType = await getProgrammingTypesPersistence.ExecuteAsync(programmingFiltersDto.Type);

        if (programmingType.Name != programmingFiltersDto.RestrictedType &&
            currentScheduleProgramming.ProgrammingType.Name == programmingFiltersDto.RestrictedType)
            return new(true, string.Empty, currentScheduleProgramming);

        return new(false, "Ya existe una programación en el rango de fechas seleccionado");
    }
    #endregion

}
