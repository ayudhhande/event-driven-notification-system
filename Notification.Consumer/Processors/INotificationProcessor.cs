using System;
using Notification.Domain;

namespace Notification.Consumer.Processors;

public interface INotificationProcessor
{
    Task ProcessAsync(PaymentEvent paymentEvent);
}
