using CleanAspire.Application.Features.Customers.EventHandlers;
using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.Customers.Commands;
public record DeleteCustomerCommand(string Id) : IFusionCacheRefreshRequest<Unit>, IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "customers" };
}

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger,  IApplicationDbContext context)
    {
        _context = context;
    }

    public async ValueTask<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customers = _context.Customers.Where(c => request.Id.Contains(c.Id));
        foreach (var customer in customers)
        {
            customer.AddDomainEvent(new CustomerDeletedEvent(customer));
            _context.Customers.Remove(customer);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
