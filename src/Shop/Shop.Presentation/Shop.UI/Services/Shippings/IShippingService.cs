using Common.Api;
using Shop.Query.Shippings._DTOs;
using Shop.UI.Models.Shippings;

namespace Shop.UI.Services.Shippings;

public interface IShippingService
{
    Task<ApiResult?> Create(CreateShippingCommandViewModel model);
    Task<ApiResult?> Edit(EditShippingCommandViewModel model);
    Task<ApiResult?> Remove(long shippingId);

    Task<ShippingDto?> GetById(long shippingId);
    Task<List<ShippingDto>?> GetAll();
}