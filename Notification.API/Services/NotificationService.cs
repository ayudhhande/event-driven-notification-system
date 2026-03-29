using System;
using Notification.Domain;
using Notification.Infrastructure.Persistence;

namespace Notification.API.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repo;
    public NotificationService(INotificationRepository repo)
    {
        _repo = repo;
    }

    public async Task CreateNotificationAsync(NotificationRequest request)
    {
        var notification = new NotificationEntity();
        await _repo.AddAsync(notification);
    }
    
}
