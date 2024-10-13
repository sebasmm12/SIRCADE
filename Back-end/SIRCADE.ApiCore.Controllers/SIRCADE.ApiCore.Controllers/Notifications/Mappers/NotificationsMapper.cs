using SIRCADE.ApiCore.Controllers.Notifications.Responses;
using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.ApiCore.Controllers.Notifications.Mappers;

public static class NotificationsMapper
{
    public static NotificationInfoResponse MapToNotificationInfoResponse(this UserNotification userNotification)
    {
        return new(userNotification.Id,
                   userNotification.Subject, 
                   userNotification.Message, 
                   userNotification.DeliveringDate, 
                   userNotification.Status);
    }
}