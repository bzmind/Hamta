using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.ShippingAggregate;

public class Shipping : BaseAggregateRoot
{
    public OrderShippingMethod ShippingMethod { get; private set; }
    public Money ShippingCost { get; private set; }

    public enum OrderShippingMethod { Normal, Fast }

    public Shipping(OrderShippingMethod orderShippingMethod, int shippingCost)
    {
        Guard(shippingCost);
        ShippingMethod = orderShippingMethod;
        ShippingCost = new Money(shippingCost);
    }

    public void Edit(OrderShippingMethod orderShippingMethod, int shippingCost)
    {
        Guard(shippingCost);
        ShippingMethod = orderShippingMethod;
        ShippingCost = new Money(shippingCost);
    }

    private void Guard(int shippingCost)
    {
        if (shippingCost <= 0)
            throw new OutOfRangeValueDomainException("Shipping cost should be more than zero");
    }
}