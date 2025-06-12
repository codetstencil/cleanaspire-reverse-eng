namespace CleanAspire.Application.Features.SalesOrders.EventHandlers;
public class SalesOrderUpdatedEvent : DomainEvent
{
    public SalesOrder SalesOrder { get; set; }
    public SalesOrderUpdatedEvent(SalesOrder salesOrder)
    {
        SalesOrder = salesOrder;
    }
}
