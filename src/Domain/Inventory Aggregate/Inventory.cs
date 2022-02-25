﻿using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;
using Domain.Shared.Value_Objects;

namespace Domain.Inventory_Aggregate;

public class Inventory : BaseEntity
{
    public long ProductId { get; private set; }
    public int Count { get; private set; }
    public Money Price { get; private set; }
    public string Color { get; private set; }
    public bool IsAvailable { get => Count != 0; private set { } }
    public bool IsDiscounted { get; private set; }

    public Inventory(long productId, int count, Money price, string color)
    {
        Validate(count, color);
        ProductId = productId;
        Count = count;
        Price = price;
        Color = color;
        IsAvailable = true;
        IsDiscounted = false;
    }

    public void Edit(int count, Money price, string color)
    {
        Validate(count, color);
        Count = count;
        Price = price;
        Color = color;
    }

    public void DiscountByTooman(int discountTooman)
    {
        Price = new Money(Price.Value - discountTooman);
        IsDiscounted = true;
    }

    public void DiscountByPercent(int discountPercent)
    {
        var discount = Price.Value * discountPercent / 100;
        Price = new Money(Price.Value - discount);
        IsDiscounted = true;
    }

    public void RemoveDiscount()
    {
        IsDiscounted = false;
    }

    private void Validate(int count, string color)
    {
        if (count < 0)
            throw new OutOfRangeValueDomainException("Inventory products can't be less than zero");

        NullOrEmptyDataDomainException.CheckString(color, nameof(color));
    }
}