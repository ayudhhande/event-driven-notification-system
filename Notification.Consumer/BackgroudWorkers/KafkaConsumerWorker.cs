using System;
using Confluent.Kafka;
using Notification.Consumer.Processors;
using Notification.Domain;

namespace Notification.Consumer.BackgroudWorkers;

public class KafkaConsumerWorker : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly INotificationProcessor _notificationProcessor;
    public KafkaConsumerWorker(INotificationProcessor notificationProcessor)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _notificationProcessor = notificationProcessor;

    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = _consumer.Consume(stoppingToken);
            await _notificationProcessor.ProcessAsync<PaymentEvent>(message.Message.Value);
        }
    }
}
