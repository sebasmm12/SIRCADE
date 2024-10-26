using System.Security.Claims;
using SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Mappers;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;
using SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Notifications.Entities;
using SIRCADE.ApiCore.Models.Notifications.Enums;
using SIRCADE.ApiCore.Models.Notifications.Extensions;
using SIRCADE.ApiCore.Models.Notifications.Persistence;
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
     ICountSchedulesProgrammingPersistence countSchedulesProgrammingPersistence,
     IGetNotificationsPersistence getNotificationsPersistence,
     IEmailService emailService,
     IHttpContextAccessor httpContextAccessor): ISchedulesProgrammingService
{
    private const string RestrictedType = "Reserva";

    public async Task<int> CreateAsync(ScheduleProgrammingRegisterRequest scheduleProgrammingRegisterRequest)
    {
        await ProcessValidationAsync(scheduleProgrammingRegisterRequest.MapToScheduleFiltersDto(RestrictedType), ScheduleProgrammingAction.Create);

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
        var scheduleProgramming = await getSchedulesProgrammingPersistence.ExecuteAsync(scheduleProgrammingUpdateRequest.Id, isTracked: true);

        scheduleProgrammingUpdateRequest.MapToScheduleProgramming(scheduleProgramming);

        await ProcessValidationAsync(scheduleProgramming.MapToScheduleFiltersDto(RestrictedType), ScheduleProgrammingAction.Update);

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

    private async Task ProcessValidationAsync(ScheduleProgrammingFiltersDto programmingFiltersDto, ScheduleProgrammingAction scheduleProgrammingAction)
    {
        var validationMessage = await ValidateRequestAsync(programmingFiltersDto, scheduleProgrammingAction);

        if (!validationMessage.IsValid)
            throw new(validationMessage.Message);

        if (validationMessage.Entities is not null)
        {
            var cancellationTasks = validationMessage.Entities.Select(CancelAsync);

            await Task.WhenAll(cancellationTasks);

            await SendCancellationEmailsAsync(validationMessage.Entities);
        }
    }

    private async Task<ValidateMessageDto<ScheduleProgramming>> ValidateRequestAsync(ScheduleProgrammingFiltersDto programmingFiltersDto, ScheduleProgrammingAction scheduleProgrammingAction)
    {
        // Validation for allowed maximum days of reservation
        var programmingType = await getProgrammingTypesPersistence.ExecuteAsync(programmingFiltersDto.Type);

        if (!ValidateAllowedReservations(programmingType.Name, programmingFiltersDto.StartDate))
            //return new(false, "No se pueden realizar reservas con menos de 3 días de anticipación");
            return new(false, "No se pueden realizar reservas menores a la fecha actual");

        // Validation for total reservations per day
        var isTotalByUserValid = await ValidateTotalByUserAsync(RestrictedType, programmingFiltersDto.StartDate, programmingFiltersDto.ScheduleProgrammingId);

        if(!isTotalByUserValid)
            return new(false, "El socio ya tiene una programación registrada para el día seleccionado");

        // Validation for maintenance programming in the same range of reservation programming for admin and receptionist users
        var currentScheduleProgrammings = await getSchedulesProgrammingPersistence
                                                    .ExecuteAsync(programmingFiltersDto);

        if (!currentScheduleProgrammings.Any())
            return new(true, string.Empty);

        var currentReservations = currentScheduleProgrammings.Where(scheduleProgramming => scheduleProgramming.ProgrammingType.Name == RestrictedType);

        if (programmingType.Name != programmingFiltersDto.RestrictedType)
            return new(true, string.Empty, currentReservations);

        return new(false, $"{programmingType.Name} no disponible");
    }

    private async Task<bool> ValidateTotalByUserAsync(string type, DateTime startDate, int? scheduleProgrammingId)
    {
        if(type != RestrictedType)
            return true;

        var userId = Convert.ToInt32(httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);

        var totalUserScheduleProgramming = await countSchedulesProgrammingPersistence.ExecuteAsync(userId, startDate, scheduleProgrammingId);

        return totalUserScheduleProgramming < 1;
    }

    private static bool ValidateAllowedReservations(string type, DateTime startDate)
    {
        if(type != RestrictedType)
            return true;

        //const int totalAllowedDays = 3;

        //return startDate.Date >= DateTime.Now.Date.AddDays(totalAllowedDays);
        return startDate >= DateTime.Now;
    }

    private async Task SendCancellationEmailsAsync(IEnumerable<ScheduleProgramming> reservations)
    {
        var notificationTemplates = await getNotificationsPersistence.ExecuteAsync(NotificationsExtensions.ReservationCancellationNotification);

        var notifications = reservations
                            .SelectMany(reservation => BuildReservations(reservation, notificationTemplates))
                            .ToList();

        foreach (var notification in notifications)
        {
            await emailService.SendAsync(notification);
        }
        
    }

    private static IEnumerable<UserNotification> BuildReservations(ScheduleProgramming reservation, IEnumerable<Notification> notificationTemplates)
    {
        var userNotifications = notificationTemplates.Select(notificationTemplate => ProcessTemplate(reservation, notificationTemplate));

        return userNotifications;
    }

    private static UserNotification ProcessTemplate(ScheduleProgramming reservation, Notification notificationTemplate)
    {
        var message = notificationTemplate
            .Template
            .Replace("{{CanchaDeportiva}}", reservation.SportField.Name)
            .Replace("{{FechaInicio}}", reservation.StartDate.ToString("dd/MM/yyyy h:mm:ss tt"));

        var userNotification = new UserNotification
        {
            ReceiverUserId = reservation.ClientId!.Value,
            NotificationId = notificationTemplate.Id,
            DeliveringDate = DateTime.UtcNow.AddHours(-5),
            Message = message,
            Status = NotificationStatus.Unread,
            Subject = notificationTemplate.Subject,
            Notification = notificationTemplate,
            ReceiverUser = reservation.Client!
        };

        return userNotification;
    }

    #endregion

}
