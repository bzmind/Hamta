using Common.Api;
using Shop.Query.Shippings._DTOs;
using Shop.UI.Models.Shippings;
using System.Text.Json;

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

    public async Task<ApiResult?> Create(CreateShippingCommandViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/shipping/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Edit(EditShippingCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/shipping/edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ApiResult?> Remove(long shippingId)
    {
        var result = await _client.DeleteAsync($"api/shipping/remove/{shippingId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }

    public async Task<ShippingDto?> GetById(long shippingId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<ShippingDto>>($"api/shipping/getbyid/{shippingId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<ShippingDto>?> GetAll()
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<ShippingDto>>>("api/shipping/getall", _jsonOptions);
        return result?.Data;
    }
}