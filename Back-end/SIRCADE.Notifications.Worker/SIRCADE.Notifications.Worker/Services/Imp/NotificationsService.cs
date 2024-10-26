using System.Net;
using System.Net.Mail;
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
     IGetSchedulesProgrammingPersistence getSchedulesProgrammingPersistence,
     IConfiguration configuration): INotificationsService
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

        var pushNotifications = notifications
                                .Where(notification => notification.Notification.DeliveringType == DeliveringType.PushNotification)
                                .Select(notification =>
                                {
                                    notification.Notification = null;
                                    notification.ReceiverUser = null;
                                    return notification;
                                })
                                .ToList();

        var emailNotifications = notifications
                                .Where(notification => notification.Notification.DeliveringType == DeliveringType.Email)
                                .ToList();

        await createUserNotificationsPersistence.ExecuteAsync(pushNotifications);

        await SendEmailsAsync(emailNotifications);
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

    private async Task SendEmailsAsync(IEnumerable<UserNotification> userNotifications)
    {
        var client = GetClient();

        var mailMessage = new MailMessage();

        mailMessage.From = new(configuration["EmailConfiguration:SenderEmail"]!);

        foreach (var user in userNotifications)
        {
            mailMessage.To.Add(user.ReceiverUser.Detail.Email!);
        }

        mailMessage.Subject = userNotifications.First().Subject;
        mailMessage.Body = userNotifications.First().Message;

        await client.SendMailAsync(mailMessage);
    }

    private SmtpClient GetClient()
    {
        var smtp = configuration["EmailConfiguration:SmtpClient"];
        var port = int.Parse(configuration["EmailConfiguration:Port"]!);
        var email = configuration["EmailConfiguration:SenderEmail"];
        var password = configuration["EmailConfiguration:SenderPassword"];

        var client = new SmtpClient(smtp, port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(email, password)
        };

        return client;
    }
}