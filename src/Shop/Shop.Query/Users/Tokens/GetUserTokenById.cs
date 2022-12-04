using Common.Query.BaseClasses;
using Dapper;
using Shop.Infrastructure;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users.Tokens;

public record GetUserTokenById(long TokenId) : IBaseQuery<UserTokenDto?>;

public class GetUserTokenByIdHandler : IBaseQueryHandler<GetUserTokenById, UserTokenDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserTokenByIdHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserTokenDto?> Handle(GetUserTokenById request, CancellationToken cancellationToken)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT TOP 1 * FROM {_dapperContext.UserTokens} WHERE Id = @TokenId";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { request.TokenId });
    }
}