using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.SellerAggregate;

public class SellerInventory : BaseEntity
{
    public long SellerId { get; private set; }
    public long ProductId { get; private set; }
    public long ColorId { get; private set; }
    public int Quantity { get; private set; }

    private Money _originalPrice;
    public Money Price { get; private set; }
    public bool IsAvailable { get => Quantity != 0; private set { } }

    private int _discountAmount;
    public int DiscountPercentage { get; private set; }
    public bool IsDiscounted { get; private set; }


    private SellerInventory()
    {

    }

    public SellerInventory(long sellerId, long productId, int quantity, int price, long colorId,
        int discountPercentage)
    {
        Guard(quantity);
        SellerId = sellerId;
        ProductId = productId;
        Quantity = quantity;
        _originalPrice = new Money(price);
        ColorId = colorId;
        IsAvailable = true;
        IsDiscounted = discountPercentage > 0;
        SetDiscountPercentage(discountPercentage);
    }

    public void Edit(long productId, int quantity, int price, long colorId, int discountPercentage)
    {
        Guard(quantity);
        ProductId = productId;
        Quantity = quantity;
        _originalPrice = new Money(price);
        ColorId = colorId;
        IsDiscounted = discountPercentage > 0;
        SetDiscountPercentage(discountPercentage);
    }

    public void IncreaseQuantity(int quantity) => Quantity += quantity;

    public void DecreaseQuantity(int quantity)
    {
        if (Quantity - quantity < 0)
            throw new OperationNotAllowedDomainException("Inventory doesn't have enough quantity");
        Quantity -= quantity;
    }

    private void SetDiscountPercentage(int discountPercentage)
    {
        if (discountPercentage is < 0 or > 100)
            throw new OutOfRangeValueDomainException("Discount percentage must be between 0 and 100");

        RemoveDiscount();
        var discountAmount = Price.Value * discountPercentage / 100;
        _discountAmount = discountAmount;
        Price = new Money(Price.Value - discountAmount);
        DiscountPercentage = discountPercentage;
    }

    private void RemoveDiscount()
    {
        if (DiscountPercentage > 0)
            DiscountPercentage = 0;

        Price = _originalPrice;
        _discountAmount = 0;
    }

    private void Guard(int count)
    {
        if (count < 0)
            throw new OutOfRangeValueDomainException("Inventory quantity should be more than zero");
    }
}