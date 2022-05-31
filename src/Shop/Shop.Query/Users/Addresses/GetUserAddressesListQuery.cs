using Common.Query.BaseClasses;
using Dapper;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users.Addresses;

public record GetUserAddressesListQuery(long UserId) : IBaseQuery<List<UserAddressDto>>;

public class GetUserAddressesListQueryHandler : IBaseQueryHandler<GetUserAddressesListQuery, List<UserAddressDto>>
{
    private readonly DapperContext _dapperContext;

    public GetUserAddressesListQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<UserAddressDto>> Handle(GetUserAddressesListQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT * FROM {_dapperContext.UserAddresses} WHERE UserId = @UserId";
        var result = await connection.QueryAsync<UserAddressDto>(sql, new { request.UserId });
        return result.ToList();
    }
}