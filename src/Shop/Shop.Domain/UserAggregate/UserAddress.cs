using Common.Domain.BaseClasses;
using Common.Domain.ValueObjects;

namespace Shop.Domain.UserAggregate;

public class UserAddress : BaseAddress
{
    public long UserId { get; private set; }
    public bool IsActive { get; private set; }

    private UserAddress()
    {

    }

    public UserAddress(long userId, string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        Guard(fullName, province, city, fullAddress, postalCode);
        UserId = userId;
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