using Common.Domain.BaseClasses;

namespace Shop.Domain.ProductAggregate;

public class ProductSpecification : BaseSpecification
{
    public long ProductId { get; private set; }

    public ProductSpecification(long productId, string title, string description, bool isImportantFeature)
    {
        Guard(title, description);
        ProductId = productId;
        Title = title;
        Description = description;
        IsImportantFeature = isImportantFeature;
    }
}