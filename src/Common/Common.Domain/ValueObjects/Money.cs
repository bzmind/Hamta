﻿using Common.Domain.Exceptions;

namespace Common.Domain.ValueObjects;

public class Money : BaseValueObject
{
    public int Value { get; private set; }

    private Money()
    {

    }

    public Money(int toomanPrice)
    {
        Guard(toomanPrice);
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

    private void Guard(int value)
    {
        if (value < 0)
            throw new InvalidDataDomainException("Invalid value: price can't be negative");
    }
}