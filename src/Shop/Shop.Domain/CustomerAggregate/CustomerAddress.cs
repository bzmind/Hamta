using Common.Domain.BaseClasses;
using Common.Domain.ValueObjects;

namespace Shop.Domain.CustomerAggregate;

public class CustomerAddress : BaseAddress
{
    public long CustomerId { get; private set; }
    public bool IsActive { get; private set; }

    private CustomerAddress()
    {

    }

    public CustomerAddress(long customerId, string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        Guard(fullName, province, city, fullAddress, postalCode);
        CustomerId = customerId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Province = province;
        City = city;
        FullAddress = fullAddress;
        PostalCode = postalCode;
    }

    public void SetAddressActivation(bool activate)
    {
        IsActive = activate;
    }
}