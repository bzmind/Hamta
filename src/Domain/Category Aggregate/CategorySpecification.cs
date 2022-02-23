using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;

namespace Domain.Category_Aggregate;

public class CategorySpecification : BaseEntity
{
    public long CategoryId { get; private set; }
    public string Title { get; private set; }

    public CategorySpecification(long categoryId, string title)
    {
        CategoryId = categoryId;
        Title = title;
    }

    public void Edit(string title)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        Title = title;
    }
}