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
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<NotificationEntity>()
            .Property(x => x.Status)
            .HasConversion<string>();
    }
}
