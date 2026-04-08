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
        var entity = new NotificationEntity
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            EventType = request.EventType,
            Message = request.Message,
            Status = PaymentStatus.PENDING,
            CreatedAt = DateTime.UtcNow
        };

        var paymentEntity = new PaymentEvent
        {
            EventId = Guid.NewGuid(),
            UserId = request.UserId,
            EventType = request.EventType,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(entity);

        //TODO: Publish event to kafka
    }
    
}
