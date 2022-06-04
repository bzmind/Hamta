using Common.Query.BaseClasses;

namespace Shop.Query.Users._DTOs;

public class UserTokenDto : BaseDto
{
    public long UserId { get; set; }
    public string JwtTokenHash { get; set; }
    public string RefreshTokenHash { get; set; }
    public DateTime JwtTokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public string Device { get; set; }
}