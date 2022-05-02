using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Shop.Domain.OrderAggregate;

public class OrderItem : BaseEntity
{
    public long OrderId { get; private set; }
    public long InventoryId { get; private set; }
    public int Count { get; private set; }
    public Money Price { get; private set; }

    public int TotalPrice => Price.Value * Count;

    private OrderItem()
    {

    }

    public OrderItem(long orderId, long inventoryId, int count, Money price)
    {
        ValidateCount(count);
        ValidatePrice(price.Value);
        OrderId = orderId;
        InventoryId = inventoryId;
        Count = count;
        Price = price;
    }

    public void IncreaseCount() => Count++;

    public void DecreaseCount()
    {
        if (Count == 1)
            throw new OperationNotAllowedDomainException("Order item count cannot be less than zero");

        Count--;
    }

    public void SetPrice(Money price)
    {
        ValidatePrice(price.Value);
        Price = price;
    }

    private void ValidateCount(int count)
    {
        if (count <= 0)
            throw new InvalidDataDomainException("Order item count cannot be zero or less than zero");
    }

    private void ValidatePrice(int price)
    {
        if (price <= 0)
            throw new InvalidDataDomainException("Order item price cannot be zero or less than zero");
    }
}