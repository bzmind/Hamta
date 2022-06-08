using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.InventoryAggregate;

namespace Shop.Infrastructure.Persistence.EF.Inventories;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories", "inventory");

        builder.Property(inventory => inventory.Id)
            .UseIdentityColumn(1);

        builder.Property(inventory => inventory.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(inventory => inventory.Quantity)
            .IsRequired();

        builder.OwnsOne(inventory => inventory.Price, options =>
        {
            options.Property(price => price.Value)
                .HasColumnName("Price")
                .IsRequired();
        });

        builder.Property(inventory => inventory.IsAvailable)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(inventory => inventory.DiscountPercentage)
            .IsRequired();

        builder.Property(inventory => inventory.IsDiscounted)
            .IsRequired()
            .HasColumnType("bit");
    }
}