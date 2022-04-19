using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.OrderAggregate.ValueObjects;

public class ShippingInfo : BaseValueObject
{
    public string ShippingMethod { get; private set; }
    public Money ShippingCost { get; private set; }

    public ShippingInfo(string shippingMethod, Money shippingCost)
    {
        Guard(shippingMethod);
        ShippingMethod = shippingMethod;
        ShippingCost = shippingCost;
    }

    private void Guard(string shippingMethod)
    {
        NullOrEmptyDataDomainException.CheckString(shippingMethod, nameof(shippingMethod));
    }
}