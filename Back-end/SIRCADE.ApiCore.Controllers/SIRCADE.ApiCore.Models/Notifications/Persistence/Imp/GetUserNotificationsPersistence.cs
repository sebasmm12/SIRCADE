﻿using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Notifications.Entities;

namespace SIRCADE.ApiCore.Models.Notifications.Persistence.Imp;

public class GetUserNotificationsPersistence(ApplicationDbContext applicationDbContext) : IGetUserNotificationsPersistence
{
    public async Task<IEnumerable<UserNotification>> ExecuteAsync(IEnumerable<int> reservationIds, DateTime deliveringDate)
    {
        var userNotifications = await applicationDbContext
                                        .UserNotifications
                                        .Where(userNotification => reservationIds.Contains(userNotification.ScheduleProgrammingId ?? 0) &&
                                                                   userNotification.DeliveringDate.Date == deliveringDate)
                                        .ToListAsync();

        return userNotifications;
    }

    public async Task<IEnumerable<UserNotification>> ExecuteAsync(int userId)
    {
        var userNotifications = await applicationDbContext
                                        .UserNotifications
                                        .Where(userNotification => userNotification.ReceiverUserId == userId)
                                        .ToListAsync();

        return userNotifications;
    }
}