using Microsoft.AspNetCore.Mvc;
using SIRCADE.ApiCore.Controllers.Notifications.Services;

namespace SIRCADE.ApiCore.Controllers.Notifications.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationsController
    (INotificationsService notificationsService): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var notifications = await notificationsService.GetAllAsync();

        return Ok(notifications);
    }
}