namespace Shop.API.ViewModels.Users.Tokens;

public class RemoveUserTokenCommand
{
    public long UserId { get; set; }
    public long TokenId { get; set; }
}