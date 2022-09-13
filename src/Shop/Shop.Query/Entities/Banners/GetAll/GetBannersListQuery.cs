using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Entities._DTOs;

namespace Shop.Query.Entities.Banners.GetAll;

public record GetBannersListQuery : IBaseQuery<List<BannerDto>>;

public class GetBannersListQueryHandler : IBaseQueryHandler<GetBannersListQuery, List<BannerDto>>
{
    private readonly ShopContext _shopContext;

    public GetBannersListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<BannerDto>> Handle(GetBannersListQuery request, CancellationToken cancellationToken)
    {
        return await _shopContext.Banners.OrderByDescending(d => d.Id)
            .Select(banner => new BannerDto
            {
                Id = banner.Id,
                CreationDate = banner.CreationDate,
                Image = banner.Image,
                Link = banner.Link,
                Position = banner.Position
            }).ToListAsync(cancellationToken);
    }
}