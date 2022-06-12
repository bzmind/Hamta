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
        var user = await _shopContext.Users
            .FirstOrDefaultAsync(c => c.PhoneNumber.Value == request.EmailOrPhone
                                      || c.Email == request.EmailOrPhone, cancellationToken);
        return user.MapToUserDto();
    }
}