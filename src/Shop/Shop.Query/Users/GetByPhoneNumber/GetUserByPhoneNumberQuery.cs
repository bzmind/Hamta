using Common.Domain.ValueObjects;
using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Users._DTOs;
using Shop.Query.Users._Mappers;

namespace Shop.Query.Users.GetByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IBaseQuery<UserDto?>;

public class GetUserByPhoneNumberQueryHandler : IBaseQueryHandler<GetUserByPhoneNumberQuery, UserDto?>
{
    private readonly ShopContext _shopContext;

    public GetUserByPhoneNumberQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var phoneNumber = new PhoneNumber(request.PhoneNumber);

        var user =
            await _shopContext.Users.FirstOrDefaultAsync(c => c.PhoneNumber.Value == phoneNumber.Value,
                cancellationToken);

        return user.MapToUserDto();
    }
}