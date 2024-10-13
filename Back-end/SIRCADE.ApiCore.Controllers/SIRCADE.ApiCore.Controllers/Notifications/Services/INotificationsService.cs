using SIRCADE.ApiCore.Controllers.Notifications.Responses;

namespace SIRCADE.ApiCore.Controllers.Notifications.Services;

public interface INotificationsService
{
    Task<IEnumerable<NotificationInfoResponse>> GetAllAsync();
}