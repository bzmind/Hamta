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
            .UseHiLo("ProductHiLoSequence")
            .UseIdentityColumn(1);

        builder.Property(product => product.CreationDate)
            .HasColumnType("datetime2(0)");

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

        builder.OwnsMany(product => product.Scores, options =>
        {
            options.ToTable("Scores", "product");

            options.Property(score => score.Value)
                .IsRequired()
                .HasColumnType("float(2,1)");
        });

        builder.OwnsOne(product => product.MainImage, options =>
        {
            options.ToTable("Images", "product");

            options.HasKey(mainImage => mainImage.Id);

            options.Property(mainImage => mainImage.Id)
                .UseIdentityColumn(1);

            options.Property(mainImage => mainImage.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(mainImage => mainImage.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsMany(product => product.GalleryImages, options =>
        {
            options.ToTable("GalleryImages", "product");

            options.HasKey(galleryImage => galleryImage.Id);

            options.Property(galleryImage => galleryImage.Id)
                .UseIdentityColumn(1);

            options.Property(galleryImage => galleryImage.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(galleryImage => galleryImage.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsMany(product => product.CustomSpecifications, options =>
        {
            options.ToTable("CustomSpecifications", "product");

            options.HasKey(customSpec => customSpec.Id);

            options.Property(customSpec => customSpec.Id)
                .UseIdentityColumn(1);

            options.Property(customSpec => customSpec.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(customSpec => customSpec.Title)
                .IsRequired()
                .HasMaxLength(50);

            options.Property(customSpec => customSpec.Description)
                .IsRequired()
                .HasMaxLength(300);
        });

        builder.OwnsMany(product => product.ExtraDescriptions, options =>
        {
            options.ToTable("ExtraDescriptions", "product");

            options.HasKey(extraDescription => extraDescription.Id);

            options.Property(extraDescription => extraDescription.Id)
                .UseIdentityColumn(1);

            options.Property(extraDescription => extraDescription.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(extraDescription => extraDescription.Title)
                .IsRequired()
                .HasMaxLength(100);

            options.Property(extraDescription => extraDescription.Description)
                .IsRequired()
                .HasMaxLength(2000);
        });
    }
}