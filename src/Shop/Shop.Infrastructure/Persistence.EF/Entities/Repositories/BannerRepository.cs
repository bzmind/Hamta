using Shop.Domain.Entities;
using Shop.Domain.Entities.Repositories;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Entities.Repositories;

public class BannerRepository : BaseRepository<Banner>, IBannerRepository
{
    public BannerRepository(ShopContext context) : base(context)
    {
    }

    public void RemoveBanner(Banner banner)
    {
        Context.Banners.Remove(banner);
    }
}