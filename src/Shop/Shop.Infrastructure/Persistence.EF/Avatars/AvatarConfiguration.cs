using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.AvatarAggregate;

namespace Shop.Infrastructure.Persistence.EF.Avatars;

public class AvatarConfiguration : IEntityTypeConfiguration<Avatar>
{
    public void Configure(EntityTypeBuilder<Avatar> builder)
    {
        builder.ToTable("Avatars", "avatar");

        builder.Property(avatar => avatar.Id)
            .UseIdentityColumn(1);

        builder.Property(avatar => avatar.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(avatar => avatar.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(avatar => avatar.Gender)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(10);
    }
}