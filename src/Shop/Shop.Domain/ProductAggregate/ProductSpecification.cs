using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProductAggregate;

public class ProductSpecification : BaseEntity
{
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public ProductSpecification(long productId, string title, string description)
    {
        Guard(title, description);
        ProductId = productId;
        Title = title;
        Description = description;
    }

    private void Guard(string title, string description)
    {
        NullOrEmptyDataDomainException.CheckString(title, nameof(title));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}