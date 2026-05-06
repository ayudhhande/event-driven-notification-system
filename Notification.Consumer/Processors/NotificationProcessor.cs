using System;
using System.Text.Json;
using Notification.Consumer.Processors;

namespace Notification.Consumer;

public class NotificationProcessor : INotificationProcessor
{
    public async Task ProcessAsync<PaymentEvent>(string message)
    {
        if(message == null)
            return;
        var json = JsonSerializer.Deserialize<PaymentEvent>(message);
        Console.WriteLine("Received message from kafka: {0}", json);
    }
}
