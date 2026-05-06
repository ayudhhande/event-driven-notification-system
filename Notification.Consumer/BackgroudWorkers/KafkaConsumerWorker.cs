using System;
using System.Text.Json;
using Confluent.Kafka;
using Notification.Consumer.Processors;
using Notification.Domain;

namespace Notification.Consumer.BackgroudWorkers;

public class KafkaConsumerWorker : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<KafkaConsumerWorker> _logger;
    public KafkaConsumerWorker(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory, ILogger<KafkaConsumerWorker> logger)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "notification-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        var topic = configuration["Kafka:Topic"];
        if(string.IsNullOrWhiteSpace(topic))
            throw new Exception("Kafka topic is not configured");
        _consumer.Subscribe(topic);

    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var processor = scope.ServiceProvider.GetRequiredService<INotificationProcessor>();
                    var message = _consumer.Consume(stoppingToken);
                    var paymentEvent = JsonSerializer.Deserialize<PaymentEvent>(message.Message.Value);
                    if(paymentEvent == null){
                        _logger.LogInformation("Received null payment event");
                        continue;
                    }
                    await processor.ProcessAsync(paymentEvent);
                    _logger.LogInformation("Event {EventId} received by Kafka consumer with message {Message}", paymentEvent.EventId, paymentEvent.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError("ExecuteAsync failed for KafkaConsumerWorker- Error: {Message}", ex.Message);
                }
                
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogError("Kafka consumer stopping");
        }
        finally
        {
            _consumer.Close();
        }
    }
}
