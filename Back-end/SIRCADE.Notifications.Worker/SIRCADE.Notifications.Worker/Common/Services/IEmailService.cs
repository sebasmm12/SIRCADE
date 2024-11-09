using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.Notifications.Worker.Common.Services;

public interface IEmailService
{
    Task SendAsync(UserNotification notifications);
}