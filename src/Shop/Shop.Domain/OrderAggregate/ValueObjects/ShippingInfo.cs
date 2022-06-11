using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.OrderAggregate.ValueObjects;

public class ShippingInfo : BaseValueObject
{
    public string ShippingName { get; private set; }
    public Money ShippingCost { get; private set; }

    private ShippingInfo()
    {

    }

    public ShippingInfo(string shippingName, Money shippingCost)
    {
        Guard(shippingName);
        ShippingName = shippingName;
        ShippingCost = shippingCost;
    }

    private void Guard(string shippingMethod)
    {
        NullOrEmptyDataDomainException.CheckString(shippingMethod, nameof(shippingMethod));
    }
}