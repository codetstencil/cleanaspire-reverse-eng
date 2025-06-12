using CleanAspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanAspire.Infrastructure.Persistence.Configurations;
public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        /// <summary>
        /// Configures the Title property of the Album entity.
        /// </summary>
        /// 
        builder.ToTable("Albums");

        builder.HasKey(x => x.AlbumId);

        builder.Property(x => x.AlbumId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .HasMaxLength(100).IsRequired();

        builder.HasOne(x => x.Artist)
            .WithMany(a => a.Albums)
            .HasForeignKey(x => x.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
