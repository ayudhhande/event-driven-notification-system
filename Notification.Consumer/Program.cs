using Microsoft.EntityFrameworkCore;
using Notification.Consumer;
using Notification.Consumer.BackgroudWorkers;
using Notification.Consumer.Processors;
using Notification.Infrastructure.Persistence;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddScoped<INotificationProcessor, NotificationProcessor>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddDbContext<NotificationDbContext>(option => option.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddHostedService<KafkaConsumerWorker>();

var host = builder.Build();
host.Run();
