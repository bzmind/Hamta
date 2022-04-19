using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Domain.CategoryAggregate;

namespace Shop.Infrastructure.Persistence.EF.Categories;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", "Category");

        builder.Property(category => category.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(category => category.Slug)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(category => category.SubCategories)
            .WithOne()
            .HasForeignKey(category => category.ParentId);
    }
}