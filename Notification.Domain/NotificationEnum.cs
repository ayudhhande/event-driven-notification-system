namespace Notification.Domain;

public enum PaymentStatus
    {
        PENDING,
        SENT,
        FAILED,
        DEAD_LETTERED
    }

    public enum PaymentEventType
    {
        PaymentSuccess,
        Failed,
        Pending
    }
