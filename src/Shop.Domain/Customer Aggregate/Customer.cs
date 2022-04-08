using System.Collections.ObjectModel;
using Common.Domain.Base_Classes;
using Common.Domain.Exceptions;
using Common.Domain.Value_Objects;

namespace Shop.Domain.Customer_Aggregate;

public class Customer : BaseAggregateRoot
{
    public string FullName { get; private set; }
    public string Email { get; private set; }

    private readonly List<CustomerAddress> _addresses = new List<CustomerAddress>();
    public ReadOnlyCollection<CustomerAddress> Addresses => _addresses.AsReadOnly();
    public PhoneNumber PhoneNumber { get; private set; }
    public string? Avatar { get; private set; }
    public bool IsSubscribedToNews { get; private set; }

    private readonly List<CustomerFavoriteItem> _favoriteItems = new List<CustomerFavoriteItem>();
    public ReadOnlyCollection<CustomerFavoriteItem> FavoriteItems => _favoriteItems.AsReadOnly();

    public Customer(string fullName, string email, PhoneNumber phoneNumber)
    {
        Guard(fullName, email);
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        IsSubscribedToNews = false;
    }

    public void Edit(string fullName, string email, PhoneNumber phoneNumber)
    {
        Guard(fullName, email);
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void ActivateAddress(long addressId)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new InvalidDataDomainException("Address was not found");

        if (address.IsActive)
            return;

        address.ActivateAddress();
    }

    public void AddAddress(long customerId, string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        _addresses.Add(new CustomerAddress(customerId, fullName, phoneNumber, province, city, fullAddress,
            postalCode));
    }

    public void EditAddress(long addressId, string fullName, PhoneNumber phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new NullOrEmptyDataDomainException("Address was not found");

        address.Edit(fullName, phoneNumber, province, city, fullAddress, postalCode);
    }

    public void RemoveAddress(long addressId)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new NullOrEmptyDataDomainException("Address was not found");

        _addresses.Remove(address);
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

    public void SetSubscriptionToNews(bool subscription)
    {
        IsSubscribedToNews = subscription;
    }

    public void AddFavoriteItem(CustomerFavoriteItem favoriteItem)
    {
        _favoriteItems.Add(favoriteItem);
    }

    public void RemoveFavoriteItem(long favoriteItemId)
    {
        var favoriteItem = FavoriteItems.FirstOrDefault(fi => fi.Id == favoriteItemId);

        if (favoriteItem == null)
            throw new NullOrEmptyDataDomainException("Favorite item not found");

        _favoriteItems.Remove(favoriteItem);
    }

    private void Guard(string fullName, string email)
    {
        NullOrEmptyDataDomainException.CheckString(fullName, nameof(fullName));
        NullOrEmptyDataDomainException.CheckString(email, nameof(email));
    }
}