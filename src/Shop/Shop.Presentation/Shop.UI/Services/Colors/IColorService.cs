using Common.Api;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;
using Shop.Query.Colors._DTOs;

namespace Shop.UI.Services.Colors;

public interface IColorService
{
    Task<ApiResult?> Create(CreateColorCommand model);
    Task<ApiResult?> Edit(EditColorCommand model);

    Task<ColorDto?> GetById(long colorId);
    Task<List<ColorDto>> GetByFilter(ColorFilterParams filterParams);
}