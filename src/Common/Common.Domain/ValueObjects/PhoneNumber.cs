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

        if (phoneNumber.Length != 11)
            throw new InvalidDataDomainException("Phone number must be 11 characters");
    }
}