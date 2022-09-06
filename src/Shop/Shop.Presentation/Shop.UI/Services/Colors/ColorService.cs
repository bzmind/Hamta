using Common.Api;
using Shop.Query.Colors._DTOs;
using System.Text.Json;
using Shop.API.ViewModels.Colors;

namespace Shop.UI.Services.Colors;

public class ColorService : BaseService, IColorService
{
    protected override string ApiEndpointName { get; set; } = "Color";
    
    public ColorService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateColorViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> Edit(EditColorViewModel model)
    {
        return await PutAsJsonAsync("Edit", model);
    }

    public async Task<ColorDto?> GetById(long colorId)
    {
        var result = await GetFromJsonAsync<ColorDto>($"GetById/{colorId}");
        return result.Data;
    }

    public async Task<ColorFilterResult> GetByFilter(ColorFilterParams filterParams)
    {
        var url = $"GetByFilter?PageId={filterParams.PageId}" +
                  $"&Take={filterParams.Take}&Name={filterParams.Name}&Code={filterParams.Code}";
        var result = await GetFromJsonAsync<ColorFilterResult>(url);
        return result.Data;
    }
}