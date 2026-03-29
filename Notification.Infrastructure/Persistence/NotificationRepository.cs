using System;
using Notification.Domain;

namespace Notification.Infrastructure.Persistence;

public class NotificationRepository : INotificationRepository
{
    private readonly NotificationDbContext _notificationDbContext;
    public NotificationRepository(NotificationDbContext notificationDbContext)
    {
        _notificationDbContext = notificationDbContext;
    }

    public async Task AddAsync(NotificationEntity notification)
    {
        _notificationDbContext.Notifications.Add(notification);
        await _notificationDbContext.SaveChangesAsync();
    }
}
