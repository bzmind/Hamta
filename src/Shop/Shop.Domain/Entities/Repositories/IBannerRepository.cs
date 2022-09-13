using Common.Domain.Repository;

namespace Shop.Domain.Entities.Repositories;

public interface IBannerRepository : IBaseRepository<Banner>
{
    void RemoveBanner(Banner banner);
}