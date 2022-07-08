using Common.Api;
using Shop.Query.Shippings._DTOs;
using System.Text.Json;
using Shop.Application.Shippings.Create;
using Shop.Application.Shippings.Edit;

namespace Shop.UI.Services.Shippings;

public class ShippingService : IShippingService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public ShippingService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateShippingCommand model)
    {
        var result = await _client.PostAsJsonAsync("api/shipping/Create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditShippingCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/shipping/Edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long shippingId)
    {
        var result = await _client.DeleteAsync($"api/shipping/Remove/{shippingId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ShippingDto?> GetById(long shippingId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<ShippingDto>>($"api/shipping/GetById/{shippingId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<ShippingDto>> GetAll()
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<ShippingDto>>>("api/shipping/GetAll", _jsonOptions);
        return result.Data;
    }
}