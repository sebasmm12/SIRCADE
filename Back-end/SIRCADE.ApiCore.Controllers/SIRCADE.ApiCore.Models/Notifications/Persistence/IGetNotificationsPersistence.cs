using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.ApiCore.Models.Notifications.Persistence;

public interface IGetNotificationsPersistence
{
    Task<IEnumerable<Notification>> ExecuteAsync(string notificationType);
}