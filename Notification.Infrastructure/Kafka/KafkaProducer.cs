using System;
using Confluent.Kafka;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notification.Domain;

namespace Notification.Infrastructure.Kafka;

public class KafkaProducer: IKafkaProducer
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<string> _logger;
    public IProducer<Null, string> _producer;

    public KafkaProducer(IConfiguration configuration, ILogger<string> logger)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"]
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
        _configuration = configuration;
        _logger = logger;
    }

    public async Task PublishAsync<PaymentEvent>(PaymentEvent message)
    {
        try
        {
            var json = JsonSerializer.Serialize(message);
            string topic = _configuration["Kafka:Topic"] ?? string.Empty;
            await _producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = json
            });
            _logger.LogInformation("Publishing message to Kafka {0}", message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish event");
            throw;
        }
        
    }
}
