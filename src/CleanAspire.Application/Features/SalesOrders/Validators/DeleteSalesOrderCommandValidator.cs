using CleanAspire.Application.Features.SalesOrders.Commands;

namespace CleanAspire.Application.Features.SalesOrders.Validators;
public class DeleteSalesOrderCommandValidator : AbstractValidator<DeleteSalesOrderCommand>
{
    public DeleteSalesOrderCommandValidator()
    {
        RuleFor(command => command.Id)
        .NotEmpty().WithMessage("At least one customer ID is required.")
        .Must(id => id != null)
        .WithMessage("SalesOrder IDs must not be empty or whitespace.");
    }
}
