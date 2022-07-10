using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;
using Shop.Query.Users._Mappers;

namespace Shop.Query.Users.GetByEmailOrPhone;

public record GetUserByEmailOrPhoneQuery(string EmailOrPhone) : IBaseQuery<UserDto?>;

public class GetUserByEmailOrPhoneQueryHandler : IBaseQueryHandler<GetUserByEmailOrPhoneQuery, UserDto?>
{
    private readonly ShopContext _shopContext;

    public GetUserByEmailOrPhoneQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<UserDto?> Handle(GetUserByEmailOrPhoneQuery request, CancellationToken cancellationToken)
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
            .FirstOrDefaultAsync(tables => tables.user.PhoneNumber.Value == request.EmailOrPhone ||
                                         tables.user.Email == request.EmailOrPhone, cancellationToken);
        return tables.user.MapToUserDto(tables.avatar);
    }
}