using System;
using Confluent.Kafka;
using System.Text.Json;

namespace Notification.Infrastructure.Kafka;

public class KafkaProducer: IKafkaProducer
{
    public IProducer<Null, string> _producer;
    public const string Topic = "payment-events";

    public KafkaProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task PublishAsync<T>(T message)
    {
        var json = JsonSerializer.Serialize(message);

        await _producer.ProduceAsync(Topic, new Message<Null, string>
        {
            Value = json
        });
        Console.WriteLine($"Publishing message to Kafka: {json}");
    }
}
