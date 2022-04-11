﻿using System.Collections.ObjectModel;
using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;
using Common.Domain.Value_Objects;

namespace Shop.Domain.OrderAggregate;

public class Order : BaseAggregateRoot
{
    public long CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderAddress? Address { get; private set; }

    private readonly List<OrderItem> _items;
    public ReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public OrderShippingMethod ShippingMethod { get; private set; }

    public Money ShippingCost
    {
        get
        {
            if (ShippingMethod == OrderShippingMethod.Fast)
                return new Money(FastShippingCost);

            return new Money(NormalShippingCost);
        }
        private set { }
    }

    public int TotalPrice
    {
        get
        {
            var price = Items.Sum(orderItem => orderItem.TotalPrice);
            return price + ShippingCost.Value;
        }
        private set { }
    }

    public enum OrderStatus { Pending, Preparing, Sending, Received }
    public enum OrderShippingMethod { Normal, Fast }

    private const int FastShippingCost = 20000;
    private const int NormalShippingCost = 0;

    public Order(long customerId, List<OrderItem> items)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        _items = items;
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
            throw new InvalidDataDomainException($"No orderItem was found with this ID: {orderItemId}");

        _items.Remove(orderItem);
    }

    public void IncreaseItemCount(long orderItemId)
    {
        CheckOrderStatus();
        var orderItem = Items.FirstOrDefault(oi => oi.Id == orderItemId);

        if (orderItem == null)
            throw new InvalidDataDomainException($"No orderItem was found with this ID: {orderItemId}");

        orderItem.IncreaseCount();
    }

    public void DecreaseItemCount(long orderItemId)
    {
        CheckOrderStatus();
        var orderItem = Items.FirstOrDefault(oi => oi.Id == orderItemId);

        if (orderItem == null)
            throw new InvalidDataDomainException($"No orderItem was found with this ID: {orderItemId}");

        orderItem.DecreaseCount();
    }

    public void SetStatus(OrderStatus orderStatus)
    {
        Status = orderStatus;
    }

    public void Checkout(OrderAddress address, OrderShippingMethod shippingMethod)
    {
        CheckOrderStatus();
        Address = address;
        ShippingMethod = shippingMethod;
        Status = OrderStatus.Preparing;
    }

    private void CheckOrderStatus()
    {
        if (Status != OrderStatus.Pending)
            throw new OperationNotAllowedDomainException("Cannot edit order details, order is already sent");
    }
}