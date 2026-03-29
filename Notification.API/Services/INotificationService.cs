using System;
using Notification.Domain;

namespace Notification.API.Services;

public interface INotificationService
{
    Task CreateNotificationAsync(NotificationRequest request);
}
