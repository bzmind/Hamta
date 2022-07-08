using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Avatars._DTOs;
using Shop.Query.Avatars._Mappers;

namespace Shop.Query.Avatars.GetAll;

public record GetAvatarsListQuery : IBaseQuery<List<AvatarDto>>;

public class GetAvatarsListQueryHandler : IBaseQueryHandler<GetAvatarsListQuery, List<AvatarDto>>
{
    private readonly ShopContext _shopContext;

    public GetAvatarsListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<AvatarDto>> Handle(GetAvatarsListQuery request, CancellationToken cancellationToken)
    {
        var avatars = await _shopContext.Avatars.OrderByDescending(a => a.Id).ToListAsync(cancellationToken);
        return avatars.MapToAvatarDto();
    }
}