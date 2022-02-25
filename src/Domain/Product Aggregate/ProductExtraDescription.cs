using Domain.Shared.BaseClasses;

namespace Domain.Product_Aggregate;

public class ProductExtraDescription : BaseEntity
{
    public long ProductId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
}