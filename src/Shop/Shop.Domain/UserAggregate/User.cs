using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Domain.UserAggregate;

public class User : BaseAggregateRoot
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private readonly List<UserAddress> _addresses = new();
    public IEnumerable<UserAddress> Addresses => _addresses.ToList();
    public PhoneNumber PhoneNumber { get; private set; }
    public string AvatarName { get; private set; } = DefaultAvatarName;
    public bool IsSubscribedToNews { get; private set; }

    private readonly List<UserFavoriteItem> _favoriteItems = new();
    public IEnumerable<UserFavoriteItem> FavoriteItems => _favoriteItems.ToList();

    public const string DefaultAvatarName = "avatar.png";

    private User()
    {

    }

    public User(string fullName, string email, string password, string phoneNumber,
        IUserDomainService userDomainService)
    {
        Guard(fullName, phoneNumber, email, userDomainService);
        NullOrEmptyDataDomainException.CheckString(password, nameof(password));
        FullName = fullName;
        Email = email;
        Password = password;
        PhoneNumber = new PhoneNumber(phoneNumber);
        IsSubscribedToNews = false;
    }

    public void Edit(string fullName, string email, string phoneNumber,
        IUserDomainService userDomainService)
    {
        Guard(fullName, phoneNumber, email, userDomainService);
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

    public void AddAddress(long userId, string fullName, string phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        _addresses.Add(new UserAddress(userId, fullName, new PhoneNumber(phoneNumber), province, city, fullAddress,
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

    public void AddFavoriteItem(UserFavoriteItem favoriteItem)
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
        IUserDomainService userDomainService)
    {
        NullOrEmptyDataDomainException.CheckString(fullName, nameof(fullName));
        NullOrEmptyDataDomainException.CheckString(phoneNumber, nameof(phoneNumber));
        userDomainService.IsPhoneNumberDuplicate(phoneNumber);
        NullOrEmptyDataDomainException.CheckString(email, nameof(email));
        userDomainService.IsEmailDuplicate(email);
    }
}