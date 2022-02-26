using Domain.Shared.BaseClasses;
using Domain.Shared.Exceptions;
using Domain.Shared.Value_Objects;

namespace Domain.Customer_Aggregate;

public class Customer : BaseAggregateRoot
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public List<CustomerAddress> Addresses { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string? Avatar { get; private set; }
    public string? CreditCardNumber { get; private set; }
    public bool IsSubscribedToNews { get; private set; }
    public List<CustomerFavoriteItem> FavoriteItems { get; private set; }

    public Customer(string fullName, string email, PhoneNumber phoneNumber)
    {
        Validate(fullName, email);
        FullName = fullName;
        Email = email;
        Addresses = new List<CustomerAddress>();
        PhoneNumber = phoneNumber;
        IsSubscribedToNews = false;
        FavoriteItems = new List<CustomerFavoriteItem>();
    }

    public void Edit(string fullName, string email, PhoneNumber phoneNumber)
    {
        Validate(fullName, email);
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void ActivateAddress(long addressId)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new InvalidDataDomainException($"No address was found with this ID: {addressId}");

        if (address.IsActive)
            return;

        address.ActivateAddress();
    }

    public void AddAddress(CustomerAddress address)
    {
        Addresses.Add(address);
    }

    public void EditAddress(long addressId, string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new NullOrEmptyDataDomainException($"No address was found with this ID: {addressId}");

        address.Edit(fullName, phoneNumber, province, city, fullAddress, postalCode);
    }

    public void RemoveAddress(long addressId)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new NullOrEmptyDataDomainException($"No address was found with this ID: {addressId}");

        Addresses.Remove(address);
    }

    public void SetAvatar(string avatar)
    {
        NullOrEmptyDataDomainException.CheckString(avatar, nameof(avatar));
        Avatar = avatar;
    }

    public void RemoveAvatar()
    {
        Avatar = null;
    }

    public void SetCreditCardNumber(string creditCardNumber)
    {
        NullOrEmptyDataDomainException.CheckString(creditCardNumber, nameof(creditCardNumber));
        CreditCardNumber = creditCardNumber;
    }

    public void RemoveCreditCardNumber()
    {
        CreditCardNumber = null;
    }

    public void SetSubscriptionToNews(bool subscription)
    {
        IsSubscribedToNews = subscription;
    }

    public void AddFavoriteItem(CustomerFavoriteItem favoriteItem)
    {
        FavoriteItems.Add(favoriteItem);
    }

    public void RemoveFavoriteItem(long favoriteItemId)
    {
        var favoriteItem = FavoriteItems.FirstOrDefault(fi => fi.Id == favoriteItemId);

        if (favoriteItem == null)
            throw new NullOrEmptyDataDomainException($"No favorite item was found with this ID: {favoriteItemId}");

        FavoriteItems.Remove(favoriteItem);
    }

    private void Validate(string fullName, string email)
    {
        NullOrEmptyDataDomainException.CheckString(fullName, nameof(fullName));
        NullOrEmptyDataDomainException.CheckString(email, nameof(email));
    }
}