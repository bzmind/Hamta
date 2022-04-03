using Common.Domain.BaseClasses;

namespace Shop.Domain.Customer_Aggregate;

public class CustomerFavoriteItem : BaseEntity
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
}