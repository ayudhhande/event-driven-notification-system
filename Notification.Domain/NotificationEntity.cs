using System;
using System.Text.RegularExpressions;

namespace Notification.Domain
{

    /* notifications
    id (UUID)
    user_id
    event_type (PaymentSuccess / Failed)
    message
    status (PENDING / SENT / FAILED)
    created_at
    processed_at
    retry_count
    */
    public class NotificationEntity
    {
        public Guid Id { get; set; } //uuid
        public int UserId { get; set; }
        public PaymentEventType EventType { get; set; }
        public string Message { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedAt { get; set; }
        public int RetryCount { get; set; }
    }
}