using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.ProductAggregate;

public class ProductGalleryImage : BaseEntity
{
    public long ProductId { get; private set; }
    public string Name { get; private set; }
    public int Sequence { get; private set; }

    public ProductGalleryImage(long productId, string name, int sequence)
    {
        Guard(name);
        ProductId = productId;
        Name = name;
        Sequence = sequence;
    }

    public void Edit(string name, int sequence)
    {
        Guard(name);
        Name = name;
        Sequence = sequence;
    }

    private void Guard(string name)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));
    }
}