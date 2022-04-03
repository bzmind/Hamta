using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.Product_Aggregate;

public class ProductAnswer : BaseEntity
{
    public long ProductId { get; private set; }
    public long CustomerId { get; private set; }
    public long ParentId { get; private set; }
    public string ParentDescription { get; private set; }
    public string Description { get; private set; }

    public ProductAnswer(long productId, long customerId, long parentId,
        string parentDescription,string description)
    {
        NullOrEmptyDataDomainException.CheckString(parentDescription, nameof(parentDescription));
        NullOrEmptyDataDomainException.CheckString(description, nameof(description));
        ProductId = productId;
        CustomerId = customerId;
        ParentId = parentId;
        ParentDescription = parentDescription;
        Description = description;
    }
}