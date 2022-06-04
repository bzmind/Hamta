using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ColorAggregate;

namespace Shop.Infrastructure.Persistence.EF.Colors;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.ToTable("Colors", "color");

        builder.Property(color => color.Id)
            .UseIdentityColumn(1);

        builder.Property(color => color.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(color => color.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(color => color.Code)
            .IsRequired()
            .HasColumnType("char")
            .HasMaxLength(7);
    }
}