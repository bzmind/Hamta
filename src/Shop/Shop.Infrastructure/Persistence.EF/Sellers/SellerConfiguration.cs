using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.SellerAggregate;

namespace Shop.Infrastructure.Persistence.EF.Sellers;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Sellers", "seller");

        builder.Property(seller => seller.Id)
            .UseIdentityColumn(1);

        builder.Property(seller => seller.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(seller => seller.ShopName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(seller => seller.NationalCode)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(seller => seller.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(10);

        builder.OwnsMany(seller => seller.Inventories, options =>
        {
            options.ToTable("Inventories", "seller");

            options.HasKey(inventory => inventory.Id);

            options.Property(inventory => inventory.Id)
                .UseIdentityColumn(1);

            options.Property(inventory => inventory.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(inventory => inventory.Quantity)
                .IsRequired();

            options.OwnsOne(inventory => inventory.Price, option =>
            {
                option.Property(price => price.Value)
                    .HasColumnName("Price")
                    .IsRequired();
            });

            options.Property(inventory => inventory.IsAvailable)
                .IsRequired()
                .HasColumnType("bit");

            options.Property(inventory => inventory.DiscountPercentage)
                .IsRequired();

            options.Property(inventory => inventory.IsDiscounted)
                .IsRequired()
                .HasColumnType("bit");
        });
    }
}