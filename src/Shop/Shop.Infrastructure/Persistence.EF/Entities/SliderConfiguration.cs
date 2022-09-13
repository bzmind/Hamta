using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.EF.Entities;

public class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.ToTable("Sliders", "entities");

        builder.HasKey(slider => slider.Id);

        builder.Property(slider => slider.Id)
            .UseIdentityColumn(1);

        builder.Property(slider => slider.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(slider => slider.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(slider => slider.Link)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(slider => slider.Image)
            .IsRequired()
            .HasMaxLength(100);
    }
}