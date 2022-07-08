using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Avatars._DTOs;
using Shop.Query.Avatars._Mappers;

namespace Shop.Query.Avatars.GetById;

public record GetAvatarByIdQuery(long AvatarId) : IBaseQuery<AvatarDto?>;

public class GetAvatarByIdQueryHandler : IBaseQueryHandler<GetAvatarByIdQuery, AvatarDto?>
{
    private readonly ShopContext _shopContext;

    public GetAvatarByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<AvatarDto?> Handle(GetAvatarByIdQuery request, CancellationToken cancellationToken)
    {
        var avatar = await _shopContext.Avatars.FirstOrDefaultAsync(a => a.Id == request.AvatarId, cancellationToken);
        return avatar.MapToAvatarDto();
    }
}