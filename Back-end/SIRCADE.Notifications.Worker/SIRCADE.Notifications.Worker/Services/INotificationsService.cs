namespace SIRCADE.Notifications.Worker.Services;

public interface INotificationsService
{
    Task SendReservationRemindersAsync(string notificationType);
}