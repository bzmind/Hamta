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
            .HasMaxLength(150);

        builder.Property(product => product.EnglishName)
            .HasMaxLength(150);

        builder.Property(product => product.Slug)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(product => product.Introduction)
            .HasMaxLength(2000);

        builder.Property(product => product.Review)
            .HasMaxLength(10000);

        builder.Property(product => product.MainImage)
            .IsRequired()
            .HasMaxLength(100);

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

            options.Property(galleryImage => galleryImage.Sequence)
                .IsRequired();
        });

        builder.OwnsMany(product => product.Specifications, options =>
        {
            options.ToTable("Specifications", "product");

            options.HasKey(specification => specification.Id);

            options.Property(specification => specification.Id)
                .UseIdentityColumn(1);

            options.Property(specification => specification.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(specification => specification.Title)
                .IsRequired()
                .HasMaxLength(50);

            options.Property(specification => specification.Description)
                .IsRequired()
                .HasMaxLength(300);
        });

        builder.OwnsMany(product => product.CategorySpecifications, options =>
        {
            options.ToTable("CategorySpecifications", "product");

            options.HasKey(specification => specification.Id);

            options.Property(specification => specification.Id)
                .UseIdentityColumn(1);

            options.Property(specification => specification.CreationDate)
                .HasColumnType("datetime2(0)");

            options.Property(specification => specification.Description)
                .IsRequired()
                .HasMaxLength(300);
        });
    }
}