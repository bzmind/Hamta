using Domain.Shared.BaseClasses;

namespace Domain.Product_Aggregate;

public class ProductSpecification : BaseEntity
{
    public long ProductId { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public bool IsImportantFeature { get; private set; }
}