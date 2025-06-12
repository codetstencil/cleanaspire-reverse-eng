using CleanAspire.Application.Features.SalesOrders.Commands;

namespace CleanAspire.Application.Features.SalesOrders.Validators;
public class CreateSalesOrderCommandValidator : AbstractValidator<CreateSalesOrderCommand>
{
    public CreateSalesOrderCommandValidator()
    {
        RuleFor(command => command.Price)
            .GreaterThan(0).WithMessage("Total amount must be greater than zero.");
        RuleFor(command => command.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
