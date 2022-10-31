using Common.Api;
using Shop.API.ViewModels.Entities.Slider;
using Shop.Query.Entities._DTOs;

namespace Shop.UI.Services.Entities.Sliders;

public interface ISliderService
{
    Task<ApiResult> Create(CreateSliderViewModel model);
    Task<ApiResult> Edit(EditSliderViewModel model);
    Task<ApiResult> Remove(long id);

    Task<ApiResult<SliderDto?>> GetById(long id);
    Task<ApiResult<List<SliderDto>>> GetAll();
}