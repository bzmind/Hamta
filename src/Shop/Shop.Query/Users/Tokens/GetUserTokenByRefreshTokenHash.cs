using Common.Query.BaseClasses;
using Dapper;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users.Tokens;

public record GetUserTokenByRefreshTokenHash(string RefreshTokenHash) : IBaseQuery<UserTokenDto?>;

public class GetUserTokenByRefreshTokenHashHandler : IBaseQueryHandler<GetUserTokenByRefreshTokenHash, UserTokenDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserTokenByRefreshTokenHashHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto?> Handle(GetUserTokenByRefreshTokenHash request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT TOP 1 * FROM {_dapperContext.UserTokens} WHERE RefreshTokenHash = @RefreshTokenHash";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { request.RefreshTokenHash });
    }
}