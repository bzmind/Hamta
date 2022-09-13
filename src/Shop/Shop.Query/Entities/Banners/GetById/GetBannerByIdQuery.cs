using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Entities._DTOs;

namespace Shop.Query.Entities.Banners.GetById;

public record GetBannerByIdQuery(long BannerId) : IBaseQuery<BannerDto?>;

public class GetBannerByIdQueryHandler : IBaseQueryHandler<GetBannerByIdQuery, BannerDto?>
{
    private readonly ShopContext _context;

    public GetBannerByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<BannerDto?> Handle(GetBannerByIdQuery request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(f => f.Id == request.BannerId, cancellationToken);
        if (banner == null)
            return null;

        return new BannerDto
        {
            Id = banner.Id,
            CreationDate = banner.CreationDate,
            Image = banner.Image,
            Link = banner.Link,
            Position = banner.Position
        };
    }
}