using System;
using Notification.Domain;

namespace Notification.Infrastructure.Kafka;

public interface IDlqProducer
{
    Task PublishAsync(PaymentEvent message);
}
