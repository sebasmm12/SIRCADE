using SIRCADE.ApiCore.Models.Notifications.Entities;
using SIRCADE.ApiCore.Models.Notifications.Enums;
using SIRCADE.ApiCore.Models.Notifications.Persistence;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

namespace SIRCADE.Notifications.Worker.Services.Imp;

public class NotificationsService
    (IGetNotificationsPersistence getNotificationsPersistence,
     IGetUserNotificationsPersistence getUserNotificationsPersistence,
     ICreateUserNotificationsPersistence createUserNotificationsPersistence,
     IGetSchedulesProgrammingPersistence getSchedulesProgrammingPersistence): INotificationsService
{

    public async Task SendReservationRemindersAsync(string notificationType)
    {
        var currentDate = DateTime
                            .UtcNow
                            .AddHours(-5)
                            .Date;

        var reminderDate = currentDate.AddDays(1);

        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(reminderDate);

        if (!reservations.Any())
            return;

        var reservationClientIds = reservations
                                    .Select(reservation => reservation.ClientId!.Value)
                                    .Distinct()
                                    .ToList();

        var receiverNotifications = await getUserNotificationsPersistence.ExecuteAsync(reservationClientIds, currentDate);

        var newReservations = reservations
                                .ExceptBy(receiverNotifications
                                    .Select(receiverNotification => receiverNotification.ReceiverUserId),
                                    reservation => reservation.ClientId!.Value)
                                .ToList();

        if (!newReservations.Any())
            return;

        await SendAsync(newReservations, notificationType);
    }

    private async Task SendAsync(IEnumerable<ScheduleProgramming> reservations, string notificationType)
    {
        var notificationTemplates = await getNotificationsPersistence.ExecuteAsync(notificationType);

        var notifications = reservations.SelectMany(reservation => BuildReservations(reservation, notificationTemplates));

        await createUserNotificationsPersistence.ExecuteAsync(notifications);
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
            Subject = notificationTemplate.Subject
        };

        return userNotification;
    }
}