using Microsoft.EntityFrameworkCore;
using Notification.API.Services;
using Notification.Infrastructure;
using Notification.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<NotificationDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
