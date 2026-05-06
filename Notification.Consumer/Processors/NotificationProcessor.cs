using System;
using System.Text.Json;
using Notification.Consumer.Processors;
using Notification.Domain;

namespace Notification.Consumer;

public class NotificationProcessor : INotificationProcessor
{
    public async Task ProcessAsync(PaymentEvent paymentEvent)
    {
        if(paymentEvent == null)
            return;
        Console.WriteLine("Received message from kafka: {0}", paymentEvent);
    }
}
