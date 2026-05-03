using System;

namespace Notification.Infrastructure.Kafka;

public interface IKafkaProducer
{
    Task PublishAsync<T>(T message);
}
