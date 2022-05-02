using Common.Domain.Exceptions;

namespace Common.Domain.ValueObjects;

public class PhoneNumber : BaseValueObject
{
    public string Value { get; private set; }

    private PhoneNumber()
    {

    }

    public PhoneNumber(string phoneNumber)
    {
        Guard(phoneNumber);
        Value = phoneNumber;
    }

    private void Guard(string phoneNumber)
    {
        NullOrEmptyDataDomainException.CheckString(phoneNumber, nameof(phoneNumber));

        if (phoneNumber.Length is > 11 or < 11)
            throw new InvalidDataDomainException("Phone number cannot be greater or less than 11 characters");
    }
}