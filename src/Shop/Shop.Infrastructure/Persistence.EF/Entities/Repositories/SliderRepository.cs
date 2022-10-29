using Shop.Domain.Entities;
using Shop.Domain.Entities.Repositories;
using Shop.Infrastructure.BaseClasses;

namespace Shop.Infrastructure.Persistence.EF.Entities.Repositories;

public class SliderRepository : BaseRepository<Slider>, ISliderRepository
{
    public SliderRepository(ShopContext shopContext) : base(shopContext)
    {
    }

    public void RemoveSlider(Slider slider)
    {
        ShopContext.Sliders.Remove(slider);
    }
}