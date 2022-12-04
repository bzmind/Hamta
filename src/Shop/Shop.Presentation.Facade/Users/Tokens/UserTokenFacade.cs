using Common.Application;
using Common.Application.Utility.Security;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Users.Tokens.AddToken;
using Shop.Application.Users.Tokens.RemoveToken;
using Shop.Presentation.Facade.Caching;
using Shop.Query.Users._DTOs;
using Shop.Query.Users.Tokens;

namespace Shop.Presentation.Facade.Users.Tokens;

public class UserTokenFacade : IUserTokenFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public UserTokenFacade(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<OperationResult> AddToken(AddUserTokenCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.UserToken(command.JwtToken.ToSHA256()));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveToken(RemoveUserTokenCommand command)
    {
        var token = await GetTokenById(command.TokenId);
        if (token != null)
            await _cache.RemoveAsync(CacheKeys.UserToken(token.JwtTokenHash));
        return await _mediator.Send(command);
    }
    
    public async Task<UserTokenDto?> GetTokenByRefreshTokenHash(string refreshToken)
    {
        return await _cache.GetOrSet(CacheKeys.UserToken(refreshToken.ToSHA256()),
            async () => await _mediator.Send(new GetUserTokenByRefreshTokenHash(refreshToken.ToSHA256())));
    }

    public async Task<UserTokenDto?> GetTokenByJwtTokenHash(string jwtToken)
    {
        return await _cache.GetOrSet(CacheKeys.UserToken(jwtToken.ToSHA256()),
            async () => await _mediator.Send(new GetUserTokenByJwtToken(jwtToken.ToSHA256())));
    }

    public async Task<UserTokenDto?> GetTokenById(long tokenId)
    {
        return await _mediator.Send(new GetUserTokenById(tokenId));
    }
}