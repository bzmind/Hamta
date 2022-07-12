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

        builder.Property(user => user.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(user => user.FullName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(user => user.Email)
            .HasMaxLength(250);

        builder.Property(user => user.Password)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(user => user.PhoneNumber, config =>
        {
            config.Property(phoneNumber => phoneNumber.Value)
                .HasColumnName("PhoneNumber")
                .IsRequired()
                .HasMaxLength(11);
        });

        builder.Property(user => user.Gender)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(10);

        builder.OwnsMany(user => user.Addresses, options =>
        {
            options.ToTable("Addresses", "user");

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

        builder.Property(user => user.IsSubscribedToNewsletter)
            .IsRequired()
            .HasColumnType("bit");

        builder.OwnsMany(user => user.FavoriteItems, options =>
        {
            options.ToTable("FavoriteItems", "user");

            options.HasKey(favoriteItem => favoriteItem.Id);

            options.Property(favoriteItem => favoriteItem.Id)
                .UseIdentityColumn(1);

            options.Property(favoriteItem => favoriteItem.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(favoriteItem => favoriteItem.UserId)
                .IsRequired();

            options.Property(favoriteItem => favoriteItem.ProductId)
                .IsRequired();
        });

        builder.OwnsMany(user => user.Tokens, options =>
        {
            options.ToTable("Tokens", "user");

            options.HasKey(t => t.Id);

            options.Property(t => t.Id)
                .UseIdentityColumn(1);

            options.Property(t => t.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(t => t.JwtTokenHash)
                .IsRequired()
                .HasMaxLength(250);

            options.Property(t => t.RefreshTokenHash)
                .IsRequired()
                .HasMaxLength(250);

            options.Property(t => t.Device)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsMany(user => user.Roles, options =>
        {
            options.ToTable("Roles", "user");

            options.HasKey(role => role.Id);

            options.Property(role => role.Id)
                .UseIdentityColumn(1);

            options.Property(role => role.CreationDate)
                .HasColumnType("datetime2(0)");
        });
    }
}