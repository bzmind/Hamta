using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;
using Domain.Shared.Value_Objects;

namespace Domain.Customer_Aggregate;

public class CustomerAddress : Address
{
    public long CustomerId { get; private set; }
    public bool IsActive { get; private set; }

    public CustomerAddress(long customerId, string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        Validate(fullName, province, city, fullAddress, postalCode);
        CustomerId = customerId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Province = province;
        City = city;
        FullAddress = fullAddress;
        PostalCode = postalCode;
    }

    public void Edit(string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        Validate(fullName, province, city, fullAddress, postalCode);
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Province = province;
        City = city;
        FullAddress = fullAddress;
        PostalCode = postalCode;
    }

    public void ActivateAddress()
    {
        IsActive = true;
    }

    private void Validate(string fullName, string province, string city, string fullAddress, string postalCode)
    {
        NullOrEmptyDataDomainException.CheckString(fullName, nameof(fullName));
        NullOrEmptyDataDomainException.CheckString(province, nameof(province));
        NullOrEmptyDataDomainException.CheckString(city, nameof(city));
        NullOrEmptyDataDomainException.CheckString(fullAddress, nameof(fullAddress));
        NullOrEmptyDataDomainException.CheckString(postalCode, nameof(postalCode));
    }
}