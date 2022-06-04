using Common.Domain.BaseClasses;
using Common.Domain.Exceptions;

namespace Shop.Domain.UserAggregate;

public class UserToken : BaseEntity
{
    public long UserId { get; private set; }
    public string JwtTokenHash { get; private set; }
    public string RefreshTokenHash { get; private set; }
    public DateTime JwtTokenExpireDate { get; private set; }
    public DateTime RefreshTokenExpireDate { get; private set; }
    public string Device { get; private set; }

    private UserToken()
    {
        
    }

    public UserToken(long userId, string jwtTokenHash, string refreshTokenHash, DateTime jwtTokenExpireDate,
        DateTime refreshTokenExpireDate, string device)
    {
        Guard(jwtTokenHash, refreshTokenHash, jwtTokenExpireDate, refreshTokenExpireDate, device);
        UserId = userId;
        JwtTokenHash = jwtTokenHash;
        RefreshTokenHash = refreshTokenHash;
        JwtTokenExpireDate = jwtTokenExpireDate;
        RefreshTokenExpireDate = refreshTokenExpireDate;
        Device = device;
    }

    private void Guard(string jwtTokenHash, string refreshTokenHash, DateTime jwtTokenExpireDate,
        DateTime refreshTokenExpireDate, string device)
    {
        NullOrEmptyDataDomainException.CheckString(jwtTokenHash, nameof(jwtTokenHash));
        NullOrEmptyDataDomainException.CheckString(refreshTokenHash, nameof(refreshTokenHash));
        NullOrEmptyDataDomainException.CheckString(device, nameof(device));

        if (jwtTokenExpireDate < DateTime.Now)
            throw new InvalidDataDomainException("Jwt token expiration date is invalid");

        if (refreshTokenExpireDate < jwtTokenExpireDate)
            throw new InvalidDataDomainException("Refresh token expiration date is invalid");
    }
}