using System;
using Notification.Domain;
using Notification.Infrastructure.Persistence;

namespace Notification.API.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repo;
    private readonly ILogger _logger;
    public NotificationService(INotificationRepository repo, ILogger logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task CreateNotificationAsync(NotificationRequest request)
    {
        _logger.LogInformation($"Creating notification for User {request.UserId} with event type {request.EventType}");
        var notification = new NotificationEntity();
        await _repo.AddAsync(notification);
    }
    
}
