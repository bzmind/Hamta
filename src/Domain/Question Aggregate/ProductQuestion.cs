using Domain.Shared.BaseClasses;

namespace Domain.Product_Aggregate;

public class ProductQuestion : BaseEntity
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public string Description { get; private set; }
}