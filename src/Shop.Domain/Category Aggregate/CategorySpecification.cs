using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;

namespace Shop.Domain.Category_Aggregate;

public class CategorySpecification : BaseEntity
{
    public long CategoryId { get; internal set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public CategorySpecification(long categoryId, string title, string description)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
        CategoryId = categoryId;
        Title = title;
        Description = description;
    }

    public void Edit(string title, string description)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
        Title = title;
        Description = description;
    }
}