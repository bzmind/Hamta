using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure;
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
        var tables = await _shopContext.Users
            .Join(_shopContext.Avatars,
                u => u.AvatarId,
                a => a.Id,
                (user, avatar) => new
                {
                    user,
                    avatar
                })
            .FirstOrDefaultAsync(c => c.user.Id == request.UserId, cancellationToken);

        var userDto = tables.user.MapToUserDto(tables.avatar);
        await userDto.GetFavoriteItemsDto(_dapperContext);
        await userDto.GetRolesDto(_shopContext);
        return userDto;
    }
}