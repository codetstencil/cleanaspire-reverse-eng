using CleanAspire.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanAspire.Infrastructure.Persistence.Configurations;
public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        /// <summary>
        /// Configures the CustomerId property of the SalesOrder entity.
        /// </summary>
        builder.Property(x => x.CustomerId).IsRequired(false);

        /// <summary>
        /// Configures the TotalAmount property of the SalesOrder entity.
        /// </summary>
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();

        /// <summary>
        /// Configures the Status property of the SalesOrder entity.
        /// </summary>
        builder.Property(x => x.Status).HasMaxLength(20).IsRequired();

        /// <summary>
        /// Configures the OrderDate property of the SalesOrder entity.
        /// </summary>
        builder.Property(x => x.OrderDate).IsRequired();

        /// <summary>
        /// Ignores the DomainEvents property of the SalesOrder entity.
        /// </summary>
        builder.Ignore(e => e.DomainEvents);

        /// <summary>
        /// This will make sure no sales order can be created without a customer.
        builder.HasOne(x => x.Customer)
            .WithMany(c => c.SalesOrders)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
