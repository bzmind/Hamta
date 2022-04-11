using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;

namespace Shop.Domain.OrderAggregate;

public class OrderItem : BaseEntity
{
    public long OrderId { get; private set; }
    public long InventoryId { get; private set; }
    public int Count { get; private set; }
    public int Price { get; private set; }
    public int TotalPrice { get => Price * Count; private set { } }

    public OrderItem(long orderId, long inventoryId, int count, int price)
    {
        ValidateCount(count);
        ValidatePrice(price);
        OrderId = orderId;
        InventoryId = inventoryId;
        Count = count;
        Price = price;
    }

    public void IncreaseCount() => Count++;

    public void DecreaseCount()
    {
        if (Count == 1)
            throw new OperationNotAllowedDomainException("Order item quantity cannot be less than zero");

        Count--;
    }

    public void SetPrice(int price)
    {
        ValidatePrice(price);
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