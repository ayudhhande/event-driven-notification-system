using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Internal;
using Notification.Consumer.Processors;
using Notification.Domain;
using Notification.Infrastructure.Kafka;
using Notification.Infrastructure.Persistence;

namespace Notification.Consumer;

public class NotificationProcessor : INotificationProcessor
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ILogger<NotificationProcessor> _logger;
    private readonly IDlqProducer _dlqProducer;
    public NotificationProcessor(INotificationRepository notificationRepository, ILogger<NotificationProcessor> logger, IDlqProducer dlqProducer)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;
        _dlqProducer = dlqProducer;
    }
    public async Task ProcessAsync(PaymentEvent paymentEvent)
    {
        if(paymentEvent == null)
        return;
        _logger.LogInformation("Received message from kafka: {PaymentEvent}", paymentEvent);

        NotificationEntity? notificationEntity = await _notificationRepository.GetByEventIdAsync(paymentEvent.EventId);
        if(notificationEntity == null){
            notificationEntity = new NotificationEntity
            {
                Id = Guid.NewGuid(),
                EventId = paymentEvent.EventId,
                UserId = paymentEvent.UserId,
                EventType = paymentEvent.EventType,
                Message = paymentEvent.Message,
                Status = PaymentStatus.PENDING,
                CreatedAt = paymentEvent.CreatedAt,
                ProcessedAt = null, // since the entity is created and not processed yet
                RetryCount = 0
            };
            await _notificationRepository.AddAsync(notificationEntity);
        }
        await Task.Delay(5000); //Simulate real case processing delay
        if(Random.Shared.Next(1,5) == 1) // To simulate failure
        {
            notificationEntity.RetryCount++;
            if(notificationEntity.RetryCount >= 3)
            {
                notificationEntity.Status = PaymentStatus.DEAD_LETTERED;
                notificationEntity.ProcessedAt = DateTime.UtcNow;
                await _dlqProducer.PublishAsync(paymentEvent); //Ddq should contain original event/messages not EF persistance models
                await _notificationRepository.UpdateAsync(notificationEntity); //to save DEAD_LETTERED to DB
                return; // for kafka return = processing complete
            }
            notificationEntity.Status = PaymentStatus.FAILED;
            notificationEntity.ProcessedAt = DateTime.UtcNow;
            await _notificationRepository.UpdateAsync(notificationEntity);
            
            throw new Exception("Notification provider failed"); // for kafka throw = retry
        }
        
        notificationEntity.Status = PaymentStatus.SENT;
        notificationEntity.ProcessedAt = DateTime.UtcNow;
        await _notificationRepository.UpdateAsync(notificationEntity);
    
    }
}
