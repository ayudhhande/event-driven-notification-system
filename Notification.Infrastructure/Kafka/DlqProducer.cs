using System;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notification.Domain;

namespace Notification.Infrastructure.Kafka;

public class DlqProducer : IDlqProducer
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<DlqProducer> _logger;
    private readonly IProducer<Null, string> _producer;
    public DlqProducer(IConfiguration configuration, ILogger<DlqProducer> logger)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
        _configuration = configuration;
        _logger = logger;
    }

    public async Task PublishAsync(PaymentEvent message)
    {
        var json = JsonSerializer.Serialize(message);
        string topic_dlq = _configuration["Kafka:DeadLetterTopic"] ?? "payment-events-dlq";

        await _producer.ProduceAsync(topic_dlq, new Message<Null, string>
        {
            Value = json
        });
        _logger.LogInformation("Publish event {EventId} to dlq", message.EventId);
    }

}
