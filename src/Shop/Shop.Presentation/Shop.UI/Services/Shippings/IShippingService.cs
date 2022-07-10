using Common.Api;
using Shop.Application.Shippings.Create;
using Shop.Application.Shippings.Edit;
using Shop.Query.Shippings._DTOs;

namespace Shop.UI.Services.Shippings;

public interface IShippingService
{
    Task<ApiResult> Create(CreateShippingCommand model);
    Task<ApiResult> Edit(EditShippingCommand model);
    Task<ApiResult> Remove(long shippingId);

    Task<ShippingDto> GetById(long shippingId);
    Task<List<ShippingDto>> GetAll();
}