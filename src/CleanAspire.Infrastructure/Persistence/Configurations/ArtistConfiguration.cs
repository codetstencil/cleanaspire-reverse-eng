using CleanAspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanAspire.Infrastructure.Persistence.Configurations;
public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        /// <summary>
        /// Configures the Name property of the Artist entity.
        /// </summary>
        builder.ToTable("Artists");

        builder.HasKey(a => a.ArtistId);

        builder.Property(a => a.ArtistId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(100).IsRequired();

        // One-to-many relationship: Artist → Albums
        builder.HasMany(a => a.Albums)
               .WithOne(al => al.Artist)
               .HasForeignKey(al => al.ArtistId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
