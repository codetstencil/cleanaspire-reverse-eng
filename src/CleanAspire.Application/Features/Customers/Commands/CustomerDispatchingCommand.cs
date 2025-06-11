using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.Customers.Commands;
public record CustomerDispatchingCommand : IFusionCacheRefreshRequest<Unit>, IRequiresValidation
{
    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>
    /// Gets or sets the email of the customer.
    /// </summary>
    public string Email { get; init; } = string.Empty;
    /// <summary>
    /// Gets or sets the phone number of the customer.
    /// </summary>
    public string PhoneNumber { get; init; } = string.Empty;
    public IEnumerable<string>? Tags => new[] { "customers" };
}

public class CustomerDispatchingCommandHandler : IRequestHandler<CustomerDispatchingCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public CustomerDispatchingCommandHandler(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async ValueTask<Unit> Handle(CustomerDispatchingCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
