using System;

namespace Notification.Infrastructure.Kafka;

public interface IKafkaProducer
{
    Task PublishAsync<PaymentEvent>(PaymentEvent message);
}
