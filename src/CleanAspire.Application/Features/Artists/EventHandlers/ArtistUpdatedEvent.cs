namespace CleanAspire.Application.Features.Artists.EventHandlers;
public class ArtistUpdatedEvent : DomainEvent
{
    public Artist Artist { get; }
    public ArtistUpdatedEvent(Artist artist)
    {
        Artist = artist;
    }
}
