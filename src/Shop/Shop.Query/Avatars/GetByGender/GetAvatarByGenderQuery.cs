using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.AvatarAggregate;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Avatars._DTOs;
using Shop.Query.Avatars._Mappers;

namespace Shop.Query.Avatars.GetByGender;

public record GetAvatarByGenderQuery(Avatar.AvatarGender Gender): IBaseQuery<AvatarDto>;

public class GetAvatarByGenderQueryHandler : IBaseQueryHandler<GetAvatarByGenderQuery, AvatarDto>
{
    private readonly ShopContext _shopContext;

    public GetAvatarByGenderQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<AvatarDto> Handle(GetAvatarByGenderQuery request, CancellationToken cancellationToken)
    {
        var avatar = await _shopContext.Avatars.FirstOrDefaultAsync(a => a.Gender == request.Gender,
            cancellationToken);
        return avatar.MapToAvatarDto();
    }
}