using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.ShippingAggregate;

public class Shipping : BaseAggregateRoot
{
    public string ShippingMethod { get; private set; }
    public Money ShippingCost { get; private set; }

    public Shipping(string shippingMethod, int shippingCost)
    {
        Guard(shippingMethod, shippingCost);
        ShippingMethod = shippingMethod;
        ShippingCost = new Money(shippingCost);
    }

    public void Edit(string shippingMethod, int shippingCost)
    {
        Guard(shippingMethod, shippingCost);
        ShippingMethod = shippingMethod;
        ShippingCost = new Money(shippingCost);
    }

    private void Guard(string shippingMethod, int shippingCost)
    {
        NullOrEmptyDataDomainException.CheckString(shippingMethod, nameof(shippingMethod));

        if (shippingCost <= 0)
            throw new OutOfRangeValueDomainException("Shipping cost should be more than zero");
    }
}