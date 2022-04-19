using System.Collections.ObjectModel;
using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.OrderAggregate.ValueObjects;

namespace Shop.Domain.OrderAggregate;

public class Order : BaseAggregateRoot
{
    public long CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderAddress? Address { get; private set; }
    public ShippingInfo? ShippingInfo { get; private set; }

    private readonly List<OrderItem> _items = new List<OrderItem>();
    public ReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

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

    public enum OrderStatus { Pending, Preparing, Sending, Received }

    public Order(long customerId)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending;
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        CheckOrderStatus();
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
        CheckOrderStatus();
        var orderItem = Items.FirstOrDefault(oi => oi.Id == orderItemId);

        if (orderItem == null)
            throw new InvalidDataDomainException("Order item not found in this order");

        _items.Remove(orderItem);
    }

    public void IncreaseItemCount(long orderItemId)
    {
        CheckOrderStatus();
        var orderItem = Items.FirstOrDefault(oi => oi.Id == orderItemId);

        if (orderItem == null)
            throw new InvalidDataDomainException("Order item not found in this order");

        orderItem.IncreaseCount();
    }

    public void DecreaseItemCount(long orderItemId)
    {
        CheckOrderStatus();
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
        CheckOrderStatus();
        Address = address;
        Status = OrderStatus.Preparing;
        ShippingInfo = new ShippingInfo(shippingMethod, new Money(shippingCost));
    }

    private void CheckOrderStatus()
    {
        if (Status != OrderStatus.Pending)
            throw new OperationNotAllowedDomainException("Cannot edit order, order is already sent");
    }
}