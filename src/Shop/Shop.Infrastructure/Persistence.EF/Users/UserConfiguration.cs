using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.UserAggregate;

namespace Shop.Infrastructure.Persistence.EF.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "user");

        builder.Property(user => user.Id)
            .UseIdentityColumn(1);

        builder.Property(user => user.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsMany(comment => comment.Addresses, option =>
        {
            option.ToTable("Addresses", "user");

            option.HasKey(address => address.Id);

            option.Property(address => address.Id)
                .UseIdentityColumn(1);

            option.Property(address => address.FullName)
                .IsRequired()
                .HasMaxLength(100);

            option.OwnsOne(address => address.PhoneNumber, config =>
            {
                config.Property(phoneNumber => phoneNumber.Value)
                    .HasColumnName("PhoneNumber")
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

        builder.OwnsOne(user => user.PhoneNumber, config =>
        {
            config.Property(phoneNumber => phoneNumber.Value)
                .HasColumnName("PhoneNumber")
                .IsRequired()
                .HasMaxLength(11);
        });

        builder.Property(user => user.AvatarName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.IsSubscribedToNews)
            .IsRequired()
            .HasColumnType("bit");

        builder.OwnsMany(user => user.FavoriteItems, option =>
        {
            option.ToTable("FavoriteItems", "user");

            option.HasKey(favoriteItem => favoriteItem.Id);

            option.Property(favoriteItem => favoriteItem.Id)
                .UseIdentityColumn(1);

            option.Property(favoriteItem => favoriteItem.UserId)
                .IsRequired();

            option.Property(favoriteItem => favoriteItem.ProductId)
                .IsRequired();
        });
    }
}