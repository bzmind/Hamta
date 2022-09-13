using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Persistence.EF.Entities;

public class BannerConfiguration : IEntityTypeConfiguration<Banner>
{
    public void Configure(EntityTypeBuilder<Banner> builder)
    {
        builder.ToTable("Banners", "entities");

        builder.HasKey(banner => banner.Id);

        builder.Property(banner => banner.Id)
            .UseIdentityColumn(1);

        builder.Property(banner => banner.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(banner => banner.Link)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(banner => banner.Image)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(banner => banner.Position)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}