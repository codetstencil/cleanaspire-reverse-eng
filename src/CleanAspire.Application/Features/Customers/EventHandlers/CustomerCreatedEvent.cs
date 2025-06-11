namespace CleanAspire.Application.Features.Customers.EventHandlers;
public class CustomerCreatedEvent : DomainEvent
{
    public Customer Customer { get; set; }
    public CustomerCreatedEvent(Customer customer)
    {
        Customer = customer;
    }
}
