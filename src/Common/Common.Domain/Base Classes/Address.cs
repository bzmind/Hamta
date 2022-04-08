using Common.Domain.Exceptions;
using Common.Domain.Value_Objects;

namespace Common.Domain.Base_Classes;

public abstract class Address : BaseEntity
{
    public string FullName { get; protected set; }
    public PhoneNumber PhoneNumber { get; protected set; }
    public string Province { get; protected set; }
    public string City { get; protected set; }
    public string FullAddress { get; protected set; }
    public string PostalCode { get; protected set; }

    public void Edit(string fullName, PhoneNumber phoneNumber, string province, string city,
        string fullAddress, string postalCode)
    {
        Guard(fullName, province, city, fullAddress, postalCode);
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Province = province;
        City = city;
        FullAddress = fullAddress;
        PostalCode = postalCode;
    }
    
    protected void Guard(string fullName, string province, string city, string fullAddress, string postalCode)
    {
        NullOrEmptyDataDomainException.CheckString(fullName, nameof(fullName));
        NullOrEmptyDataDomainException.CheckString(province, nameof(province));
        NullOrEmptyDataDomainException.CheckString(city, nameof(city));
        NullOrEmptyDataDomainException.CheckString(fullAddress, nameof(fullAddress));
        NullOrEmptyDataDomainException.CheckString(postalCode, nameof(postalCode));
    }
}