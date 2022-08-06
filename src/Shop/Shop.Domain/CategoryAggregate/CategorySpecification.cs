using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CategoryAggregate;

public class CategorySpecification : BaseEntity
{
    public long CategoryId { get; private set; }
    public string Title { get; private set; }
    public bool IsImportantFeature { get; private set; }

    public CategorySpecification(long categoryId, string title, bool isImportantFeature)
    {
        Guard(title);
        CategoryId = categoryId;
        Title = title;
        IsImportantFeature = isImportantFeature;
    }

    private void Guard(string title)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
    }
}