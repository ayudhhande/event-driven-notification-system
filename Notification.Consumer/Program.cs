using Notification.Consumer;
using Notification.Consumer.BackgroudWorkers;
using Notification.Consumer.Processors;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<INotificationProcessor, NotificationProcessor>();
builder.Services.AddHostedService<KafkaConsumerWorker>();

var host = builder.Build();
host.Run();
