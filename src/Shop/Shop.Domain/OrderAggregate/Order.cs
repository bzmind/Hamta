using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.OrderAggregate.ValueObjects;

namespace Shop.Domain.OrderAggregate;

public class Order : BaseAggregateRoot
{
    public long CustomerId { get; private set; }
    public string Status { get; private set; }
    public OrderAddress? Address { get; private set; }
    public ShippingInfo? ShippingInfo { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IEnumerable<OrderItem> Items => _items.ToList();

    public enum OrderStatus { Pending, Preparing, Sending, Received }

    public int? TotalPrice
    {
        get
        {
            var itemsPrice = Items.Sum(orderItem => orderItem.TotalPrice);

            if (ShippingInfo != null)
                return itemsPrice + ShippingInfo.ShippingCost.Value;

            return itemsPrice;
        }
    }
    
    public Order(long customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending.ToString();
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        OrderEditGuard();

        var item = Items.FirstOrDefault(oi => oi.InventoryId == orderItem.InventoryId);

        if (item == null)
        {
            _items.Add(orderItem);
            return;
        }

        item.IncreaseCount();
    }

    public void RemoveOrderItem(long orderItemId)
    {
        OrderEditGuard();

        var orderItem = Items.FirstOrDefault(oi => oi.Id == orderItemId);

        if (orderItem == null)
            throw new InvalidDataDomainException("Order item not found in this order");

        _items.Remove(orderItem);
    }

    public void IncreaseItemCount(long orderItemId)
    {
        OrderEditGuard();

        var orderItem = Items.FirstOrDefault(oi => oi.Id == orderItemId);

        if (orderItem == null)
            throw new InvalidDataDomainException("Order item not found in this order");

        orderItem.IncreaseCount();
    }

    public void DecreaseItemCount(long orderItemId)
    {
        OrderEditGuard();

        var orderItem = Items.FirstOrDefault(oi => oi.Id == orderItemId);

        if (orderItem == null)
            throw new InvalidDataDomainException("Order item not found in this order");

        orderItem.DecreaseCount();
    }

    public void SetStatus(OrderStatus orderStatus)
    {
        Status = orderStatus.ToString();
    }

    public void Checkout(OrderAddress address, string shippingMethod, int shippingCost)
    {
        OrderEditGuard();

        Address = address;
        Status = OrderStatus.Preparing.ToString();
        ShippingInfo = new ShippingInfo(shippingMethod, new Money(shippingCost));
    }

    private void OrderEditGuard()
    {
        if (Status != OrderStatus.Pending.ToString())
            throw new OperationNotAllowedDomainException("Cannot edit order, order is already sent");
    }
}