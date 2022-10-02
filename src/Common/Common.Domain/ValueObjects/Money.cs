using Common.Domain.Exceptions;

namespace Common.Domain.ValueObjects;

public class Money : BaseValueObject
{
    public int Value { get; private set; }

    private Money()
    {

    }

    public Money(int value)
    {
        Guard(value);
        Value = value;
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
            throw new InvalidDataDomainException("Price value can't be less than zero");
    }
}