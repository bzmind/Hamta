using Common.Domain.BaseClasses;

namespace Shop.Domain.CategoryAggregate;

public class CategorySpecification : BaseSpecification
{
    public long CategoryId { get; private set; }

    public CategorySpecification(long categoryId, string title, string description, bool isImportantFeature)
    {
        Guard(title, description);
        CategoryId = categoryId;
        Title = title;
        Description = description;
        IsImportantFeature = isImportantFeature;
    }
}