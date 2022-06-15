using Common.Api;
using Shop.Query.Colors._DTOs;
using Shop.UI.Models.Colors;

namespace Shop.UI.Services.Colors;

public interface IColorService
{
    Task<ApiResult?> Create(CreateColorCommandViewModel model);
    Task<ApiResult?> Edit(EditColorCommandViewModel model);

    Task<ColorDto?> GetById(long colorId);
    Task<List<ColorDto>?> GetByFilter(ColorFilterParamsViewModel filterParams);
}