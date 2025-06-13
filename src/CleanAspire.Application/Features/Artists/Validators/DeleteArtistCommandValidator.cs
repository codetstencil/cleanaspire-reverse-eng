using CleanAspire.Application.Features.Artists.Commands;

namespace CleanAspire.Application.Features.Artists.Validators;
public class DeleteArtistCommandValidator : AbstractValidator<DeleteArtistCommand>
{
    public DeleteArtistCommandValidator()
    {
        RuleFor(command => command.ArtistId)
            .NotEmpty().WithMessage("At least one artist ID is required.")
            .Must(id => id != null)
            .WithMessage("Artist IDs must not be empty or whitespace.");
    }
}
