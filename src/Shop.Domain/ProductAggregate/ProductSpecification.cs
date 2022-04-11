using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProductAggregate;

public class ProductSpecification : BaseEntity
{
    public long ProductId { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public bool IsImportantFeature { get; private set; }

    public ProductSpecification(long productId, string key, string value, bool isImportantFeature)
    {
        Guard(key, value);
        ProductId = productId;
        Key = key;
        Value = value;
        IsImportantFeature = isImportantFeature;
    }

    private void Guard(string key, string value)
    {
        NullOrEmptyDataDomainException.CheckString(key, nameof(key));
        NullOrEmptyDataDomainException.CheckString(value, nameof(value));
    }
}