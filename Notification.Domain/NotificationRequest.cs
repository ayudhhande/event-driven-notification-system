using System;
using System.ComponentModel.DataAnnotations;

namespace Notification.Domain;

public class NotificationRequest
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public PaymentEventType EventType { get; set; }
    [Required]
    [MaxLength(500)]
    public string Message { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.PENDING;
}
