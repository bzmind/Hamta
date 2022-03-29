using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Domain.Product_Aggregate;

public class ProductExtraDescription : BaseEntity
{
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public ProductExtraDescription(long productId, string title, string description)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
        ProductId = productId;
        Title = title;
        Description = description;
    }
}