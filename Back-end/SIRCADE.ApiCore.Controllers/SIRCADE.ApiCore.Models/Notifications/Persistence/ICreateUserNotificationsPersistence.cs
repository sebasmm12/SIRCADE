using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.ApiCore.Models.Notifications.Persistence;

public interface ICreateUserNotificationsPersistence
{
    Task ExecuteAsync(IEnumerable<UserNotification> userNotifications);
}