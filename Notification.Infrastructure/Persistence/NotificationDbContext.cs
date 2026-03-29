using System;
using Microsoft.EntityFrameworkCore;
using Notification.Domain;

namespace Notification.Infrastructure.Persistence;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
        
    }

    public DbSet<NotificationEntity> Notifications {get;set;}
}
