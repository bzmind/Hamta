using Common.Domain.Repository;

namespace Shop.Domain.Entities.Repositories;

public interface ISliderRepository : IBaseRepository<Slider>
{
    void RemoveSlider(Slider slider);
}