using System;

namespace Notification.Domain;

public class PaymentEvent
{
    public Guid EventId{get; set;}
    public int UserId {get; set;}
    public PaymentEventType EventType {get; set;}
    public string Message {get; set;}
    public DateTime CreatedAt {get; set;} 
}
