using Common.Api;
using Shop.Query.Shippings._DTOs;
using System.Text.Json;
using Shop.Application.Shippings.Create;
using Shop.Application.Shippings.Edit;

namespace Shop.UI.Services.Shippings;

public class ShippingService : BaseService, IShippingService
{
    protected override string ApiEndpointName { get; set; } = "Shipping";

    public ShippingService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateShippingCommand model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> Edit(EditShippingCommand model)
    {
        return await PutAsJsonAsync("Edit", model);
    }

    public async Task<ApiResult> Remove(long shippingId)
    {
        return await DeleteAsync($"Remove/{shippingId}");
    }

    public async Task<ShippingDto> GetById(long shippingId)
    {
        var result = await GetFromJsonAsync<ShippingDto>($"GetById/{shippingId}");
        return result.Data;
    }

    public async Task<List<ShippingDto>> GetAll()
    {
        var result = await GetFromJsonAsync<List<ShippingDto>>("GetAll");
        return result.Data;
    }
}