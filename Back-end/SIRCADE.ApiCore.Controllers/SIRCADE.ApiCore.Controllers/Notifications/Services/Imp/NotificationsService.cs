using SIRCADE.ApiCore.Models.Notifications.Persistence;
using System.Security.Claims;
using SIRCADE.ApiCore.Controllers.Notifications.Mappers;
using SIRCADE.ApiCore.Controllers.Notifications.Responses;

namespace SIRCADE.ApiCore.Controllers.Notifications.Services.Imp;

public class NotificationsService
    (IGetUserNotificationsPersistence getUserNotificationsPersistence,
     IHttpContextAccessor httpContextAccessor) : INotificationsService
{

    public async Task<IEnumerable<NotificationInfoResponse>> GetAllAsync()
    {
        var userId = Convert.ToInt32(httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);

        var notifications = await getUserNotificationsPersistence.ExecuteAsync(userId);

        var notificationsInfoResponse = notifications.Select(NotificationsMapper.MapToNotificationInfoResponse);

        return notificationsInfoResponse;
    }
}