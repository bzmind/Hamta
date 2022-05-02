using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.ShippingAggregate;

public class Shipping : BaseAggregateRoot
{
    public string Method { get; private set; }
    public Money Cost { get; private set; }

    private Shipping()
    {

    }

    public Shipping(string method, int shippingCost)
    {
        Guard(method, shippingCost);
        Method = method;
        Cost = new Money(shippingCost);
    }

    public void Edit(string shippingMethod, int shippingCost)
    {
        Guard(shippingMethod, shippingCost);
        Method = shippingMethod;
        Cost = new Money(shippingCost);
    }

    private void Guard(string shippingMethod, int shippingCost)
    {
        NullOrEmptyDataDomainException.CheckString(shippingMethod, nameof(shippingMethod));

        if (shippingCost <= 0)
            throw new OutOfRangeValueDomainException("Shipping cost should be more than zero");
    }
}