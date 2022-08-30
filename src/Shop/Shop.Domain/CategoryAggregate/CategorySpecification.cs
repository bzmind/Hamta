using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CategoryAggregate;

public class CategorySpecification : BaseEntity
{
    public long CategoryId { get; private set; }
    public string Title { get; private set; }
    public bool IsImportant { get; private set; }
    public bool IsOptional { get; private set; }

    public CategorySpecification(long categoryId, string title, bool isImportant, bool isOptional)
    {
        Guard(title);
        CategoryId = categoryId;
        Title = title;
        IsImportant = isImportant;
        IsOptional = isOptional;
    }

    private void Guard(string title)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
    }
}