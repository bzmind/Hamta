using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.ShippingAggregate;

public class Shipping : BaseAggregateRoot
{
    public string Name { get; private set; }
    public Money Cost { get; private set; }

    private Shipping()
    {

    }

    public Shipping(string name, int cost)
    {
        Guard(name, cost);
        Name = name;
        Cost = new Money(cost);
    }

    public void Edit(string name, int cost)
    {
        Guard(name, cost);
        Name = name;
        Cost = new Money(cost);
    }

    private void Guard(string name, int cost)
    {
        NullOrEmptyDataDomainException.CheckString(name, nameof(name));

        if (cost <= 0)
            throw new OutOfRangeValueDomainException("Shipping cost should be more than zero");
    }
}