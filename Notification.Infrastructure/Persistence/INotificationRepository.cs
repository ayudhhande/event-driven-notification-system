using System;
using Notification.Domain;

namespace Notification.Infrastructure.Persistence;

public interface INotificationRepository
{
    Task AddAsync(NotificationEntity notification);
}
