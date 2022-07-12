using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAggregate;

namespace Shop.Infrastructure.Persistence.EF.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "order");

        builder.Property(order => order.Id)
            .UseHiLo("OrderHiLoSequence")
            .UseIdentityColumn(1);

        builder.Property(order => order.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(order => order.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);

        builder.OwnsOne(comment => comment.Address, options =>
        {
            options.ToTable("Addresses", "order");

            options.HasKey(address => address.Id);

            options.Property(address => address.Id)
                .UseIdentityColumn(1);

            options.Property(address => address.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(address => address.FullName)
                .IsRequired()
                .HasMaxLength(30);

            options.OwnsOne(address => address.PhoneNumber, config =>
            {
                config.Property(phoneNumber => phoneNumber.Value)
                    .HasColumnName("PhoneNumber")
                    .IsRequired()
                    .HasMaxLength(11);
            });

            options.Property(address => address.Province)
                .IsRequired()
                .HasMaxLength(30);

            options.Property(address => address.City)
                .IsRequired()
                .HasMaxLength(30);

            options.Property(address => address.FullAddress)
                .IsRequired()
                .HasMaxLength(300);

            options.Property(address => address.PostalCode)
                .IsRequired()
                .HasMaxLength(10);
        });

        builder.OwnsOne(order => order.ShippingInfo, options =>
        {
            options.Property(shippingInfo => shippingInfo.ShippingName)
                .HasColumnName("ShippingName")
                .IsRequired()
                .HasMaxLength(50);

            options.OwnsOne(shippingInfo => shippingInfo.ShippingCost, config =>
            {
                config.Property(shippingCost => shippingCost.Value)
                    .HasColumnName("ShippingCost")
                    .IsRequired();
            });
        });

        builder.OwnsMany(order => order.Items, options =>
        {
            options.ToTable("Items", "order");

            options.HasKey(item => item.Id);

            options.Property(item => item.Id)
                .UseIdentityColumn(1);

            options.Property(item => item.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(item => item.Count)
                .IsRequired();

            options.OwnsOne(item => item.Price, config =>
            {
                config.Property(price => price.Value)
                    .HasColumnName("Price")
                    .IsRequired();
            });
        });
    }
}