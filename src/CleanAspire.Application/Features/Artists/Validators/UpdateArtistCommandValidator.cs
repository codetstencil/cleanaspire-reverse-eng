using CleanAspire.Application.Features.Artists.Commands;

namespace CleanAspire.Application.Features.Artists.Validators;
public class UpdateArtistCommandValidator : AbstractValidator<UpdateArtistCommand>
{
    public UpdateArtistCommandValidator()
    {
        // Validate Artist ID (required)
        RuleFor(command => command.ArtistId)
            .NotEmpty().WithMessage("Artist ID is required.");
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Artist name is required.")
            .MaximumLength(100).WithMessage("Artist name must not exceed 100 characters.");
    }
}
