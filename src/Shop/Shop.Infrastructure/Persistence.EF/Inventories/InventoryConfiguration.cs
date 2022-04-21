using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.InventoryAggregate;

namespace Shop.Infrastructure.Persistence.EF.Inventories;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories", "inventory");

        builder.Property(inventory => inventory.Quantity)
            .IsRequired();

        builder.OwnsOne(inventory => inventory.Price, option =>
        {
            option.Property(price => price.Value)
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