using Common.Api;
using Shop.API.ViewModels.Colors;
using Shop.Query.Colors._DTOs;

namespace Shop.UI.Services.Colors;

public interface IColorService
{
    Task<ApiResult> Create(CreateColorViewModel model);
    Task<ApiResult> Edit(EditColorViewModel model);

    Task<ColorDto?> GetById(long colorId);
    Task<ColorFilterResult> GetByFilter(ColorFilterParams filterParams);
}