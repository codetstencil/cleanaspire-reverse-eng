namespace CleanAspire.Domain.Entities;

public class Album
{
    public int AlbumId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ArtistId { get; set; }
    public virtual Artist Artist { get; set; } = null!;
}
