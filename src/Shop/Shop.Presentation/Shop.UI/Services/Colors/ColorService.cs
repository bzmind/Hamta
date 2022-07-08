using Common.Api;
using Shop.Query.Colors._DTOs;
using System.Text.Json;
using Shop.Application.Colors.Create;
using Shop.Application.Colors.Edit;

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

    public async Task<ApiResult?> Create(CreateColorCommand model)
    {
        var result = await _client.PostAsJsonAsync("api/color/Create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditColorCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/color/Edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ColorDto?> GetById(long colorId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<ColorDto>>($"api/color/GetById/{colorId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<ColorDto>> GetByFilter(ColorFilterParams filterParams)
    {
        var url = $"api/color/GetByFilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&Name={filterParams.Name}&Code={filterParams.Code}";

        var result = await _client.GetFromJsonAsync<ApiResult<List<ColorDto>>>(url, _jsonOptions);
        return result.Data;
    }
}