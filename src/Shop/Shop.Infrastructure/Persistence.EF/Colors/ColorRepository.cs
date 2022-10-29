using Shop.Domain.ColorAggregate;
using Shop.Domain.ColorAggregate.Repository;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Colors;

public class ColorRepository : BaseRepository<Color>, IColorRepository
{
    public ColorRepository(ShopContext shopContext) : base(shopContext)
    {
    }
}