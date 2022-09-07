using Common.Query.BaseClasses;
using Dapper;
using Shop.Infrastructure;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users.Tokens;

public record GetUserTokenByJwtToken(string JwtTokenHash) : IBaseQuery<UserTokenDto?>;

public class GetUserTokenByJwtTokenHandler : IBaseQueryHandler<GetUserTokenByJwtToken, UserTokenDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserTokenByJwtTokenHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto?> Handle(GetUserTokenByJwtToken request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT TOP 1 * FROM {_dapperContext.UserTokens} WHERE JwtTokenHash = @JwtTokenHash";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { request.JwtTokenHash });
    }
}