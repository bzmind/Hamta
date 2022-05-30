using Common.Application;
using Shop.Application.Shippings.Create;
using Shop.Application.Shippings.Edit;
using Shop.Query.Shippings._DTOs;

namespace Shop.Presentation.Facade.Shippings;

public interface IShippingFacade
{
    Task<OperationResult<long>> Create(CreateShippingCommand command);
    Task<OperationResult> Edit(EditShippingCommand command);
    Task<OperationResult> Remove(long shippingId);

    Task<ShippingDto?> GetById(long id);
    Task<List<ShippingDto>> GetAll();
}