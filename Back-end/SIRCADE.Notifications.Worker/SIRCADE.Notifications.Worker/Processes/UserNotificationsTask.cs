using SIRCADE.ApiCore.Models.Notifications.Extensions;
using SIRCADE.Notifications.Worker.Services;

namespace SIRCADE.Notifications.Worker.Processes;

public class UserNotificationsTask(
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromSeconds(30);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(_period);

        while(!stoppingToken.IsCancellationRequested &&
              await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = serviceScopeFactory.CreateScope();

            var notificationsService = scope.ServiceProvider.GetRequiredService<INotificationsService>();

            await notificationsService.SendReservationRemindersAsync(NotificationsExtensions.ReservationReminderNotification);
        }
    }
}