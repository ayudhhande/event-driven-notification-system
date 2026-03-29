using System;

namespace Notification.Domain;

public class NotificationRequest
{
    public int UserId { get; set; }
    public PaymentEventType EventType { get; set; }
    public string Message { get; set; }
    public PaymentStatus Status { get; set; }
}
