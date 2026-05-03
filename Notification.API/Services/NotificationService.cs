using System;
using Notification.Domain;
using Notification.Infrastructure.Kafka;
using Notification.Infrastructure.Persistence;

namespace Notification.API.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repo;
    private readonly ILogger<NotificationService> _logger;
    private readonly IKafkaProducer _producer;
    public NotificationService(INotificationRepository repo, ILogger<NotificationService> logger, IKafkaProducer producer)
    {
        _repo = repo;
        _logger = logger;
        _producer = producer;
    }

    public async Task CreateNotificationAsync(NotificationRequest request)
    {
        _logger.LogInformation($"Creating notification for User {request.UserId} with event type {request.EventType}");

        var paymentEvent = new PaymentEvent
        {
            EventId = Guid.NewGuid(),
            UserId = request.UserId,
            EventType = request.EventType,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };
        
        await _producer.PublishAsync(paymentEvent);
    }
    
}
