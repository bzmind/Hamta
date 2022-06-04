using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.CategoryAggregate;

namespace Shop.Infrastructure.Persistence.EF.Categories;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", "category");

        builder.Property(category => category.Id)
            .UseHiLo("CategoryHiLoSequence")
            .UseIdentityColumn(1);

        builder.Property(category => category.CreationDate)
            .HasColumnType("datetime2(0)");

        builder.Property(category => category.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(category => category.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(category => category.SubCategories)
            .WithOne()
            .HasForeignKey(childCategory => childCategory.ParentId);

        builder.OwnsMany(category => category.Specifications, option =>
        {
            option.ToTable("Specifications", "category");

            option.HasKey(spec => spec.Id);

            option.Property(spec => spec.Id)
                .UseIdentityColumn(1);

            option.Property(spec => spec.Title)
                .IsRequired()
                .HasMaxLength(100);

            option.Property(spec => spec.Description)
                .IsRequired()
                .HasMaxLength(500);
        });
    }
}