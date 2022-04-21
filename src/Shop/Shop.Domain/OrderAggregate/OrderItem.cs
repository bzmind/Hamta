using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.OrderAggregate;

public class OrderItem : BaseEntity
{
    public long OrderId { get; private set; }
    public long InventoryId { get; private set; }
    public int Quantity { get; private set; }
    public Money Price { get; private set; }
    public int TotalPrice => Price.Value * Quantity;

    public OrderItem(long orderId, long inventoryId, int quantity, Money price)
    {
        ValidateCount(quantity);
        ValidatePrice(price.Value);
        OrderId = orderId;
        InventoryId = inventoryId;
        Quantity = quantity;
        Price = price;
    }

    public void IncreaseCount() => Quantity++;

    public void DecreaseCount()
    {
        if (Quantity == 1)
            throw new OperationNotAllowedDomainException("Order item quantity cannot be less than zero");

        Quantity--;
    }

    public void SetPrice(Money price)
    {
        ValidatePrice(price.Value);
        Price = price;
    }

    private void ValidateCount(int count)
    {
        if (count <= 0)
            throw new InvalidDataDomainException("Order item quantity cannot be zero or less than zero");
    }

    private void ValidatePrice(int price)
    {
        if (price <= 0)
            throw new InvalidDataDomainException("Order item price cannot be zero or less than zero");
    }
}