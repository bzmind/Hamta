using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.OrderAggregate.Events;
using Shop.Domain.OrderAggregate.ValueObjects;

namespace Shop.Domain.OrderAggregate;

public class Order : BaseAggregateRoot
{
    public long UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderAddress? Address { get; private set; }
    public ShippingInfo? ShippingInfo { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IEnumerable<OrderItem> Items => _items.ToList();

    public enum OrderStatus
    {
        Canceled,
        Pending,
        Preparing,
        Sending,
        Delivered
    }

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

    public Order(long userId)
    {
        UserId = userId;
        Status = OrderStatus.Pending;
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

        item.IncreaseCountBy(orderItem.Count + item.Count);
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
        Status = orderStatus;
    }

    public void Checkout(OrderAddress address, string shippingMethod, int shippingCost)
    {
        OrderEditGuard();
        Address = address;
        ShippingInfo = new ShippingInfo(shippingMethod, new Money(shippingCost));
    }

    public void Finalize()
    {
        OrderEditGuard();
        Status = OrderStatus.Preparing;
        AddDomainEvent(new OrderFinalizedEvent(Id));
    }

    private void OrderEditGuard()
    {
        if (Status != OrderStatus.Pending)
            throw new OperationNotAllowedDomainException("Cannot edit order, order is already sent");
    }
}