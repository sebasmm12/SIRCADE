using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.ApiCore.Controllers.Common.Services.Interfaces;

public interface IEmailService
{
    Task SendAsync(UserNotification notifications);
}