using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.RoleAggregate;

namespace Shop.Infrastructure.Persistence.EF.Roles;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", "role");

        builder.HasKey(role => role.Id);

        builder.Property(role => role.Id)
            .UseIdentityColumn(1);

        builder.Property(role => role.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(role => role.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsMany(role => role.Permissions, options =>
        {
            options.ToTable("Permissions", "role");

            options.HasKey(permission => permission.Id);

            options.Property(permission => permission.Id)
                .UseIdentityColumn(1);

            options.Property(permission => permission.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(permission => permission.Permission)
                .IsRequired()
                .HasMaxLength(30);
        });
    }
}