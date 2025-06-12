namespace CleanAspire.Domain.Entities;
public class Artist
{
    public int ArtistId { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
