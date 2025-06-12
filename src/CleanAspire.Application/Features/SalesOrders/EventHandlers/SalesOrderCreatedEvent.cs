namespace CleanAspire.Application.Features.SalesOrders.EventHandlers;
public class SalesOrderCreatedEvent : DomainEvent
{
    public SalesOrder SalesOrder { get; set; }
    public SalesOrderCreatedEvent(SalesOrder salesOrder)
    {
        SalesOrder = salesOrder;
    }
}
