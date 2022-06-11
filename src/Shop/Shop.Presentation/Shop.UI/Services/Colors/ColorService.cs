using System.Text.Json;
using Common.Api;
using Shop.Query.Colors._DTOs;
using Shop.UI.Models.Colors;

namespace Shop.UI.Services.Colors;

public class ColorService : IColorService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public ColorService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateColorViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/color/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditColorViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/color/edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ColorDto?> GetById(long colorId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<ColorDto>>($"api/color/getbyid/{colorId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<ColorDto>?> GetByFilter(ColorFilterParamsViewModel filterParams)
    {
        var url = $"api/color/getbyfilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&Name={filterParams.Name}&Code={filterParams.Code}";

        var result = await _client.GetFromJsonAsync<ApiResult<List<ColorDto>>>(url, _jsonOptions);
        return result?.Data;
    }
}