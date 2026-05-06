using System;

namespace Notification.Consumer.Processors;

public interface INotificationProcessor
{
    Task ProcessAsync<PaymentEvent>(string message);
}
