using CleanAspire.Application.Features.Customers.Commands;

namespace CleanAspire.Application.Features.Customers.Validators;
public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("At least one customer ID is required.")
            .Must(id => id != null)
            .WithMessage("Customer IDs must not be empty or whitespace.");
    }
}
