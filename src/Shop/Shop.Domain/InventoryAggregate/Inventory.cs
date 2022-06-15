using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.InventoryAggregate;

public class Inventory : BaseAggregateRoot
{
    public long ProductId { get; private set; }
    public long ColorId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }
    public bool IsAvailable { get => Quantity != 0; private set { } }
    public int DiscountPercentage { get; private set; }
    public bool IsDiscounted { get; private set; }

    private int _discountAmount;

    private Inventory()
    {

    }

    public Inventory(long productId, int quantity, int price, long colorId)
    {
        Guard(quantity);
        ProductId = productId;
        Quantity = quantity;
        Price = new Money(price);
        ColorId = colorId;
        IsAvailable = true;
    }

    public void Edit(long productId, int quantity, int price, long colorId)
    {
        Guard(quantity);
        ProductId = productId;
        Quantity = quantity;
        Price = new Money(price);
        ColorId = colorId;
    }

    public void IncreaseQuantity(int amount) => Quantity += amount;

    public void DecreaseQuantity(int amount)
    {
        if (Quantity - amount < 0)
            throw new OperationNotAllowedDomainException("Inventory quantity can't be less than zero");

        Quantity -= amount;
    }

    public void SetDiscountPercentage(int discountPercentage)
    {
        if (discountPercentage is < 0 or > 100)
            throw new OutOfRangeValueDomainException("Discount percentage must be between 0 and 100");

        RemoveDiscount();
        var discountAmount = Price.Value * discountPercentage / 100;
        _discountAmount = discountAmount;
        Price = new Money(Price.Value - discountAmount);
        IsDiscounted = true;
    }

    public void RemoveDiscount()
    {
        if (DiscountPercentage > 0)
            DiscountPercentage = 0;

        Price = new Money(Price.Value + _discountAmount);
        _discountAmount = 0;
        IsDiscounted = false;
    }

    private void Guard(int count)
    {
        if (count < 0)
            throw new OutOfRangeValueDomainException("Inventory products can't be less than zero");
    }
}