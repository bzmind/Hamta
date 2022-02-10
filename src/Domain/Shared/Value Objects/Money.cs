using Domain.Shared.BaseClasses;
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

    public Money DiscountByTooman(int discountTooman)
    {
        var money = new Money(Value - discountTooman);
        return money;
    }

    public Money DiscountByPercent(int discountPercent)
    {
        var discount = Value * discountPercent / 100;
        var money = new Money(Value - discount);
        return money;
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