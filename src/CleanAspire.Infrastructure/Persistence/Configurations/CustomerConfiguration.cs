using CleanAspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanAspire.Infrastructure.Persistence.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        /// <summary>
        /// Configures the Name property of the Customer entity.
        /// </summary>
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        /// <summary>
        /// Configures the Email property of the Customer entity.
        /// </summary>
        builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
        /// <summary>
        /// Configures the PhoneNumber property of the Customer entity.
        /// </summary>
        builder.Property(x => x.PhoneNumber).HasMaxLength(15).IsRequired();
        /// <summary>
        /// Configures the Address property of the Customer entity.
        /// </summary>
        builder.Property(x => x.Address).HasMaxLength(60).IsRequired();
        /// <summary>
        /// Ignores the DomainEvents property of the Customer entity.
        /// </summary>
        builder.Ignore(e => e.DomainEvents);
    }
}
