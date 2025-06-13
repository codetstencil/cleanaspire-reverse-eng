using CleanAspire.Application.Pipeline;

namespace CleanAspire.Application.Features.Artists.Commands;
public record UpdateArtistCommand(
    int ArtistId,
    string Name
) : IFusionCacheRefreshRequest<Unit>,
    IRequiresValidation
{
    public IEnumerable<string>? Tags => new[] { "artists" };
}

public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdateArtistCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async ValueTask<Unit> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists.FindAsync(new object[] { request.ArtistId }, cancellationToken);
        if (artist == null)
        {
            throw new KeyNotFoundException($"Artist with ID '{request.ArtistId}' was not found.");
        }
        artist.Name = request.Name;
        _context.Artists.Update(artist);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
