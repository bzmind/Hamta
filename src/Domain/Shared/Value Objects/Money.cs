﻿using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;

namespace Domain.Shared.Value_Objects;

public class Money : BaseValueObject
{
    public int Value { get; private set; }

    public Money(int toomanPrice)
    {
        Validate(toomanPrice);
        Value = toomanPrice;
    }
    
    public static Money operator +(Money firstMoney, Money secondMoney)
    {
        return new Money(firstMoney.Value + secondMoney.Value);
    }

    public static Money operator -(Money firstMoney, Money secondMoney)
    {
        return new Money(firstMoney.Value - secondMoney.Value);
    }

    private void Validate(int value)
    {
        if (value < 0)
            throw new InvalidDataDomainException("Invalid value: price can't be negative");
    }
}