using Common.Api;
using Shop.API.ViewModels.Entities.Slider;
using Shop.Query.Entities._DTOs;

namespace Shop.UI.Services.Entities.Sliders;

public interface ISliderService
{
    Task<ApiResult> Create(CreateSliderViewModel model);
    Task<ApiResult> Edit(EditSliderViewModel model);
    Task<ApiResult> Remove(long id);

    Task<SliderDto?> GetById(long id);
    Task<List<SliderDto>> GetAll();
}