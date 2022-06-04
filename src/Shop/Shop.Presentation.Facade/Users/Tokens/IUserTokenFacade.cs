using Common.Application;
using Shop.Application.Users.AddToken;
using Shop.Application.Users.RemoveToken;
using Shop.Query.Users._DTOs;

namespace Shop.Presentation.Facade.Users.Tokens;

public interface IUserTokenFacade
{
    Task<OperationResult> AddToken(AddUserTokenCommand command);
    Task<OperationResult> RemoveToken(RemoveUserTokenCommand command);

    Task<UserTokenDto?> GetTokenByRefreshTokenHash(string refreshToken);
    Task<UserTokenDto?> GetTokenByJwtTokenHash(string jwtToken);
}