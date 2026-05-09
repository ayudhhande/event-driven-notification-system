using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Internal;
using Notification.Consumer.Processors;
using Notification.Domain;
using Notification.Infrastructure.Persistence;

namespace Notification.Consumer;

public class NotificationProcessor : INotificationProcessor
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<NotificationProcessor> _logger;
    public NotificationProcessor(INotificationRepository notificationRepository, ILogger<NotificationProcessor> logger)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;

    }
    public async Task ProcessAsync(PaymentEvent paymentEvent)
    {
        if(paymentEvent == null)
            return;
        _logger.LogInformation("Received message from kafka: {PaymentEvent}", paymentEvent);
        var notificationEntity = new NotificationEntity
        {
            Id = Guid.NewGuid(),
            UserId = paymentEvent.UserId,
            EventType = paymentEvent.EventType,
            Message = paymentEvent.Message,
            Status = PaymentStatus.PENDING,
            CreatedAt = paymentEvent.CreatedAt,
            ProcessedAt = null // since the entity is created and not processed yet
            // RetryCount = 1
        };
        await _notificationRepository.AddAsync(notificationEntity);

        await Task.Delay(5000); //Simulate real time processing delay

        notificationEntity.ProcessedAt = DateTime.UtcNow;
        notificationEntity.Status = PaymentStatus.SENT;

        await _notificationRepository.UpdateAsync(notificationEntity);
        
    }
}
