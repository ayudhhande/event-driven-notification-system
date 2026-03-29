namespace Notification.Domain;

public enum PaymentStatus
    {
        PENDING,
        SENT,
        FAILED
    }

    public enum PaymentEventType
    {
        PaymentSuccess,
        Failed
    }
