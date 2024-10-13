using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.ApiCore.Models.Notifications.Persistence.Imp;

public class GetNotificationsPersistence(ApplicationDbContext applicationDbContext) : IGetNotificationsPersistence
{
    public async Task<IEnumerable<Notification>> ExecuteAsync(string notificationType)
    {
        var notifications = await applicationDbContext
                                        .Notifications
                                        .Where(notification => notification.Type == notificationType)
                                        .ToListAsync();

        return notifications;
    }
}