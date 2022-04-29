using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.OrderAggregate;

namespace Shop.Infrastructure.Persistence.EF.Orders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "order");

        builder.Property(order => order.Id).UseHiLo("OrderHiLoSequence");

        builder.Property(order => order.Status)
            .IsRequired()
            .HasMaxLength(20);

        builder.OwnsOne(comment => comment.Address, option =>
        {
            option.ToTable("Addresses", "order");

            option.Property(address => address.FullName)
                .IsRequired()
                .HasMaxLength(100);

            option.OwnsOne(address => address.PhoneNumber, config =>
            {
                config.Property(phoneNumber => phoneNumber.Value)
                    .IsRequired()
                    .HasMaxLength(11);
            });

            option.Property(address => address.Province)
                .IsRequired()
                .HasMaxLength(100);

            option.Property(address => address.City)
                .IsRequired()
                .HasMaxLength(100);

            option.Property(address => address.FullAddress)
                .IsRequired()
                .HasMaxLength(300);

            option.Property(address => address.PostalCode)
                .IsRequired()
                .HasMaxLength(10);
        });

        builder.OwnsOne(order => order.ShippingInfo, option =>
        {
            option.Property(shippingInfo => shippingInfo.ShippingMethod)
                .IsRequired()
                .HasMaxLength(50);

            option.Property(shippingInfo => shippingInfo.ShippingCost)
                .IsRequired();
        });

        builder.OwnsMany(order => order.Items, option =>
        {
            option.Property(item => item.Count)
                .IsRequired();

            option.OwnsOne(item => item.Price, config =>
            {
                config.Property(price => price.Value)
                    .IsRequired();
            });
        });
    }
}