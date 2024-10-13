using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.ApiCore.Models.Notifications.Persistence.Imp;

public class CreateUserNotificationsPersistence(
    ApplicationDbContext applicationDbContext) : ICreateUserNotificationsPersistence
{
    public async Task ExecuteAsync(IEnumerable<UserNotification> userNotifications)
    {
        await applicationDbContext.AddRangeAsync(userNotifications);

        await applicationDbContext.SaveChangesAsync();
    }
}