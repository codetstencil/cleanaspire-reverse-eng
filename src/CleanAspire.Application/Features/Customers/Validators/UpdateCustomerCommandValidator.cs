using CleanAspire.Application.Features.Customers.Commands;

namespace CleanAspire.Application.Features.Customers.Validators;
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        // Validate Customer ID (required)
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(100).WithMessage("Customer name must not exceed 100 characters.");

        RuleFor(command => command.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(15).WithMessage("Phone number must not exceed 15 characters.");

        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(command => command.Address)
            .MaximumLength(250).WithMessage("Address must not exceed 250 characters.");
    }
}
