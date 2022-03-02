using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;

namespace Domain.Shared.Value_Objects;

public class PhoneNumber : BaseValueObject
{
    public string Value { get; private set; }

    public PhoneNumber(string phoneNumber)
    {
        NullOrEmptyDataDomainException.CheckString(phoneNumber, nameof(phoneNumber));
        if (phoneNumber.Length is > 11 or < 11)
            throw new InvalidDataDomainException("Phone number cannot be greater or less than 11 characters");

        Value = phoneNumber;
    }
}