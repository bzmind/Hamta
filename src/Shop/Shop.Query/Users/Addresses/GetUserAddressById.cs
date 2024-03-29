﻿using Common.Query.BaseClasses;
using Dapper;
using Shop.Infrastructure;
using Shop.Query.Users._DTOs;

namespace Shop.Query.Users.Addresses;

public record GetUserAddressByIdQuery(long Id) : IBaseQuery<UserAddressDto?>;

public class GetUserAddressByIdQueryHandler : IBaseQueryHandler<GetUserAddressByIdQuery, UserAddressDto?>
{
    private readonly DapperContext _dapperContext;

    public GetUserAddressByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<UserAddressDto?> Handle(GetUserAddressByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT TOP 1 * FROM {_dapperContext.UserAddresses} WHERE Id = @Id";
        return await connection
            .QueryFirstOrDefaultAsync<UserAddressDto>(sql, new { request.Id });
    }
}