using CleanAspire.Application.Features.Artists.DTOs;
using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.Artists.Commands;
public record CreateArtistCommand(string Name) : IFusionCacheRefreshRequest<ArtistDto>, IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "artists" };
}

public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, ArtistDto>
{
    readonly IApplicationDbContext _context;
    public CreateArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<ArtistDto> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = new Artist
        {
            Name = request.Name
        };
        _context.Artists.Add(artist);
        await _context.SaveChangesAsync(cancellationToken);
        return new ArtistDto
        {
            ArtistId = artist.ArtistId,
            Name = artist.Name
        };
    }
}
