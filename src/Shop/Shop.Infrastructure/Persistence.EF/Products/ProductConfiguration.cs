using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.ProductAggregate;

namespace Shop.Infrastructure.Persistence.EF.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "product");

        builder.Property(product => product.Id)
            .UseHiLo("ProductHiLoSequence");

        builder.Property(product => product.Id)
            .UseIdentityColumn(1);

        builder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(product => product.EnglishName)
            .HasMaxLength(100);

        builder.Property(product => product.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(product => product.Description)
            .HasMaxLength(2000);

        builder.OwnsMany(product => product.Scores, option =>
        {
            option.ToTable("Scores", "product");

            option.Property(score => score.Value)
                .IsRequired()
                .HasColumnType("float(2,1)");
        });

        builder.OwnsOne(product => product.MainImage, option =>
        {
            option.ToTable("Images", "product");

            option.HasKey(mainImage => mainImage.Id);

            option.Property(mainImage => mainImage.Id)
                .UseIdentityColumn(1);

            option.Property(mainImage => mainImage.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsMany(product => product.GalleryImages, option =>
        {
            option.ToTable("GalleryImages", "product");

            option.HasKey(mainImage => mainImage.Id);

            option.Property(mainImage => mainImage.Id)
                .UseIdentityColumn(1);

            option.Property(mainImage => mainImage.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsMany(product => product.CustomSpecifications, option =>
        {
            option.ToTable("CustomSpecifications", "product");

            option.HasKey(customSpec => customSpec.Id);

            option.Property(customSpec => customSpec.Id)
                .UseIdentityColumn(1);

            option.Property(customSpec => customSpec.Title)
                .IsRequired()
                .HasMaxLength(50);

            option.Property(customSpec => customSpec.Description)
                .IsRequired()
                .HasMaxLength(300);
        });

        builder.OwnsMany(product => product.ExtraDescriptions, option =>
        {
            option.ToTable("ExtraDescriptions", "product");

            option.HasKey(extraDescription => extraDescription.Id);

            option.Property(extraDescription => extraDescription.Id)
                .UseIdentityColumn(1);

            option.Property(extraDescription => extraDescription.Title)
                .IsRequired()
                .HasMaxLength(100);

            option.Property(extraDescription => extraDescription.Description)
                .IsRequired()
                .HasMaxLength(2000);
        });
    }
}