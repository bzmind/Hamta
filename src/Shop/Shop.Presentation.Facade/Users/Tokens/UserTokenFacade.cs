using Common.Application;
using Common.Application.Utility.Security;
using MediatR;
using Shop.Application.Users.Tokens.AddToken;
using Shop.Application.Users.Tokens.RemoveToken;
using Shop.Query.Users._DTOs;
using Shop.Query.Users.Tokens;

namespace Shop.Presentation.Facade.Users.Tokens;

public class UserTokenFacade : IUserTokenFacade
{
    private readonly IMediator _mediator;

    public UserTokenFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {
        return await _mediator.Send(command);
    }
    
    public async Task<UserTokenDto?> GetTokenByRefreshTokenHash(string refreshToken)
    {
        return await _mediator.Send(new GetUserTokenByRefreshTokenHash(refreshToken.ToSHA256()));
    }

    public async Task<UserTokenDto?> GetTokenByJwtTokenHash(string jwtToken)
    {
        return await _mediator.Send(new GetUserTokenByJwtToken(jwtToken.ToSHA256()));
    }
}