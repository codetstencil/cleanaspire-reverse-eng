namespace CleanAspire.Application.Features.Artists.EventHandlers;
public class ArtistDeletedEvent : DomainEvent
{
    public Artist Artist { get; }
    public ArtistDeletedEvent(Artist artist)
    {
        Artist = artist;
    }
}
