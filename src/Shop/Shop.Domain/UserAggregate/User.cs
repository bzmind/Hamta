using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAggregate.Services;

namespace Shop.Domain.UserAggregate;

public class User : BaseAggregateRoot
{
    public string FullName { get; private set; }
    public string? Email { get; private set; }
    public string Password { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public UserGender Gender { get; private set; }
    public long AvatarId { get; private set; }

    private readonly List<UserAddress> _addresses = new();
    public IEnumerable<UserAddress> Addresses => _addresses.ToList();
    public bool IsSubscribedToNewsletter { get; private set; }

    private readonly List<UserFavoriteItem> _favoriteItems = new();
    public IEnumerable<UserFavoriteItem> FavoriteItems => _favoriteItems.ToList();

    private readonly List<UserToken> _tokens = new();
    public IEnumerable<UserToken> Tokens => _tokens.ToList();

    private readonly List<UserRole> _roles = new();
    public IEnumerable<UserRole> Roles => _roles.ToList();

    public const int PasswordMinLength = 8;
    public const int MaximumSimultaneousDevices = 3;

    public enum UserGender
    {
        Male = 1,
        Female = 2
    }

    private User()
    {

    }

    public User(string fullName, UserGender gender, string phoneNumber, string password, long avatarId,
        IUserDomainService userDomainService)
    {
        Guard(fullName, phoneNumber, userDomainService);
        PasswordGuard(password);
        FullName = fullName;
        Password = password;
        AvatarId = avatarId;
        PhoneNumber = new PhoneNumber(phoneNumber);
        Gender = gender;
        IsSubscribedToNewsletter = false;
    }

    public void Edit(string fullName, UserGender gender, string email, string phoneNumber,
        IUserDomainService userDomainService)
    {
        Guard(fullName, phoneNumber, userDomainService, email);
        FullName = fullName;
        Email = email;
        PhoneNumber = new PhoneNumber(phoneNumber);
        Gender = gender;
    }

    public static User Register(string fullName, UserGender gender, string phoneNumber, string password,
        long avatarId, IUserDomainService userDomainService)
    {
        return new User(fullName, gender, phoneNumber, password, avatarId, userDomainService);
    }

    public void AddAddress(UserAddress address)
    {
        _addresses.Add(address);
    }

    public void EditAddress(long addressId, string fullName, string phoneNumber, string province,
        string city, string fullAddress, string postalCode)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new NullOrEmptyDataDomainException("Address not found");

        address.Edit(fullName, new PhoneNumber(phoneNumber), province, city, fullAddress, postalCode);
    }

    public void SetAddressActivation(long addressId, bool activate)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new InvalidDataDomainException("Address not found");

        _addresses.ForEach(a => a.SetAddressActivation(false));
        address.SetAddressActivation(activate);
    }

    public void RemoveAddress(long addressId)
    {
        var address = Addresses.FirstOrDefault(a => a.Id == addressId);

        if (address == null)
            throw new NullOrEmptyDataDomainException("Address not found");

        _addresses.Remove(address);
    }

    public void ResetPassword(string password)
    {
        PasswordGuard(password);
        Password = password;
    }

    public void SetNewsletterSubscription()
    {
        IsSubscribedToNewsletter = !IsSubscribedToNewsletter;
    }

    public void SetAvatar(long avatarId)
    {
        AvatarId = avatarId;
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

    public void AddToken(string jwtTokenHash, string refreshTokenHash, DateTime jwtTokenExpireDate,
        DateTime refreshTokenExpireDate, string device)
    {
        var activeTokenCount = Tokens.Count(t => t.RefreshTokenExpireDate > DateTime.Now);

        if (activeTokenCount >= MaximumSimultaneousDevices)
            throw new OperationNotAllowedDomainException
                ($"You can't use more than {MaximumSimultaneousDevices} devices simultaneously");

        var token = new UserToken(Id, jwtTokenHash, refreshTokenHash, jwtTokenExpireDate,
            refreshTokenExpireDate, device);

        _tokens.Add(token);
    }

    public void RemoveToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(t => t.Id == tokenId);

        if (token == null)
            throw new DataNotFoundDomainException("Token not found");

        _tokens.Remove(token);
    }

    public void RemoveAllTokens()
    {
        _tokens.Clear();
    }

    public void AddRole(UserRole role)
    {
        _roles.Add(role);
    }

    public void RemoveRole(long roleId)
    {
        var role = _roles.FirstOrDefault(r => r.Id == roleId);

        if (role == null)
            throw new DataNotFoundDomainException("Role not found");

        _roles.Remove(role);
    }

    private void Guard(string fullName, string phoneNumber, IUserDomainService userDomainService,
        string? email = null)
    {
        NullOrEmptyDataDomainException.CheckString(fullName, nameof(fullName));
        NullOrEmptyDataDomainException.CheckString(phoneNumber, nameof(phoneNumber));

        userDomainService.IsPhoneNumberDuplicate(phoneNumber);

        if (email != null && !string.IsNullOrWhiteSpace(email))
            userDomainService.IsEmailDuplicate(email);
    }

    private void PasswordGuard(string password)
    {
        NullOrEmptyDataDomainException.CheckString(password, nameof(password));

        if (password.Length < PasswordMinLength)
            throw new InvalidDataDomainException($"Password cannot be less than {PasswordMinLength} characters");
    }
}