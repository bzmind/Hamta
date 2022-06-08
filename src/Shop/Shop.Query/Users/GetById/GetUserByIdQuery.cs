using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;
using Shop.Query.Users._Mappers;

namespace Shop.Query.Users.GetById;

public record GetUserByIdQuery(long UserId) : IBaseQuery<UserDto?>;

public class GetUserByIdQueryHandler : IBaseQueryHandler<GetUserByIdQuery, UserDto?>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetUserByIdQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _shopContext.Users.FirstOrDefaultAsync(c => c.Id == request.UserId, cancellationToken);

        var userDto = user.MapToUserDto();
        await userDto.GetFavoriteItemsDto(_dapperContext);
        await userDto.GetRolesDto(_shopContext);

        return userDto;
    }
}