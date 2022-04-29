using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.CustomerAggregate;

namespace Shop.Infrastructure.Persistence.EF.Customers;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", "customer");

        builder.Property(customer => customer.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(customer => customer.Email)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(customer => customer.Password)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsMany(comment => comment.Addresses, option =>
        {
            option.ToTable("Addresses", "customer");

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

        builder.OwnsOne(customer => customer.PhoneNumber, config =>
        {
            config.Property(phoneNumber => phoneNumber.Value)
                .IsRequired()
                .HasMaxLength(11);
        });

        builder.Property(customer => customer.AvatarName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(customer => customer.IsSubscribedToNews)
            .IsRequired()
            .HasColumnType("bit");

        builder.OwnsMany(customer => customer.FavoriteItems, option =>
        {
            option.ToTable("FavoriteItems", "customer");

            option.Property(favoriteItem => favoriteItem.CustomerId)
                .IsRequired();

            option.Property(favoriteItem => favoriteItem.ProductId)
                .IsRequired();
        });
    }
}