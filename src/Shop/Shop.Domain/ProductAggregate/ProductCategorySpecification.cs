using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProductAggregate;

public class ProductCategorySpecification : BaseEntity
{
    public long ProductId { get; private set; }
    public long CategorySpecificationId { get; private set; }
    public string Description { get; private set; }

    public ProductCategorySpecification(long productId, long categorySpecificationId, string description)
    {
        Guard(description);
        ProductId = productId;
        CategorySpecificationId = categorySpecificationId;
        Description = description;
    }

    private void Guard(string description)
    {
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
    }
}