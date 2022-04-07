using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;

namespace Shop.Domain.Product_Aggregate;

public class ProductImage : BaseEntity
{
    public long ProductId { get; private set; }
    public string Name { get; private set; }

    public ProductImage(long productId, string name)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
        ProductId = productId;
        Name = name;
    }
}