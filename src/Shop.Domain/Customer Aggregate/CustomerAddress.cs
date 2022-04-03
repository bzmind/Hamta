using Common.Domain.BaseClasses;
using Common.Domain.Value_Objects;

namespace Shop.Domain.Customer_Aggregate;

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

    public void ActivateAddress()
    {
        IsActive = true;
    }
}