﻿using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.CustomerAggregate.Services;

namespace Shop.Domain.CustomerAggregate;

public class Customer : BaseAggregateRoot
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private readonly List<CustomerAddress> _addresses = new();
    public IEnumerable<CustomerAddress> Addresses => _addresses.ToList();
    public PhoneNumber PhoneNumber { get; private set; }
    public string AvatarName { get; private set; } = DefaultAvatarName;
    public bool IsSubscribedToNews { get; private set; }

    private readonly List<CustomerFavoriteItem> _favoriteItems = new();
    public IEnumerable<CustomerFavoriteItem> FavoriteItems => _favoriteItems.ToList();

    public const string DefaultAvatarName = "avatar.png";

    private Customer()
    {

    }

    public Customer(string fullName, string email, string password, string phoneNumber,
        ICustomerDomainService customerDomainService)
    {
        Guard(fullName, phoneNumber, email, customerDomainService);
        NullOrEmptyDataDomainException.CheckString(password, nameof(password));
        FullName = fullName;
        Email = email;
        Password = password;
        PhoneNumber = new PhoneNumber(phoneNumber);
        IsSubscribedToNews = false;
    }

    public void Edit(string fullName, string email, string phoneNumber,
        ICustomerDomainService customerDomainService)
    {
        Guard(fullName, phoneNumber, email, customerDomainService);
        FullName = fullName;
        Email = email;
        PhoneNumber = new PhoneNumber(phoneNumber);
    }

    public void SetAddressActivation(long addressId, bool activate)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new InvalidDataDomainException("Address not found");

        address.SetAddressActivation(activate);
    }

    public void AddAddress(long customerId, string fullName, string phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        _addresses.Add(new CustomerAddress(customerId, fullName, new PhoneNumber(phoneNumber), province, city, fullAddress,
            postalCode));
    }

    public void EditAddress(long addressId, string fullName, string phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new NullOrEmptyDataDomainException("Address not found");

        address.Edit(fullName, new PhoneNumber(phoneNumber), province, city, fullAddress, postalCode);
    }

    public void RemoveAddress(long addressId)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new NullOrEmptyDataDomainException("Address not found");

        _addresses.Remove(address);
    }

    public void SetAvatar(string avatarName)
    {
        if (string.IsNullOrEmpty(avatarName))
            AvatarName = DefaultAvatarName;

        AvatarName = avatarName;
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

    private void Guard(string fullName, string phoneNumber, string email,
        ICustomerDomainService customerDomainService)
    {
        NullOrEmptyDataDomainException.CheckString(fullName, nameof(fullName));
        NullOrEmptyDataDomainException.CheckString(phoneNumber, nameof(phoneNumber));
        customerDomainService.IsPhoneNumberDuplicate(phoneNumber);
        NullOrEmptyDataDomainException.CheckString(email, nameof(email));
        customerDomainService.IsEmailDuplicate(email);
    }
}