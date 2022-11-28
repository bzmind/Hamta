using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.CategoryAggregate;

public class CategorySpecification : BaseEntity
{
    public long CategoryId { get; private set; }
    public string Title { get; private set; }
    public bool IsImportant { get; private set; }
    public bool IsOptional { get; private set; }
    public bool IsFilterable { get; private set; }

    private CategorySpecification()
    {
        
    }

    public CategorySpecification(long categoryId, string title, bool isImportant, bool isOptional,
        bool isFilterable)
    {
        Guard(title);
        CategoryId = categoryId;
        Title = title;
        IsImportant = isImportant;
        IsOptional = isOptional;
        IsFilterable = isFilterable;
    }

    public void Edit(string title, bool isImportant, bool isOptional, bool isFilterable)
    {
        Guard(title);
        Title = title;
        IsImportant = isImportant;
        IsOptional = isOptional;
        IsFilterable = isFilterable;
    }

    private void Guard(string title)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
    }
}