using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.Product_Aggregate;

public class ProductSpecification : BaseEntity
{
    public long ProductId { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public bool IsImportantFeature { get; private set; }

    public ProductSpecification(long productId, string key, string value, bool isImportantFeature)
    {
        NullOrEmptyDataDomainException.CheckString(key, nameof(key));
        NullOrEmptyDataDomainException.CheckString(value, nameof(value));
        ProductId = productId;
        Key = key;
        Value = value;
        IsImportantFeature = isImportantFeature;
    }
}