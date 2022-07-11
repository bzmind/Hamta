namespace Shop.API.ViewModels.Users.Tokens;

public class AddUserTokenViewModel
{
    public long UserId { get; set; }
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime JwtTokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public string Device { get; set; }
}