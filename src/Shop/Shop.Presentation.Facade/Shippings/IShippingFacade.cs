using Common.Application;
using Shop.Application.Shippings.Create;
using Shop.Application.Shippings.Edit;
using Shop.Application.Shippings.Remove;
using Shop.Query.Shippings._DTOs;

namespace Shop.Presentation.Facade.Shippings;

public interface IShippingFacade
{
    Task<OperationResult> Create(CreateShippingCommand command);
    Task<OperationResult> Edit(EditShippingCommand command);
    Task<OperationResult> Remove(RemoveShippingCommand command);

    Task<ShippingDto?> GetShippingById(long id);
    Task<List<ShippingDto>> GetShippingList();
}