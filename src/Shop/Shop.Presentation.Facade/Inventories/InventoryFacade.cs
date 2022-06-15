using Common.Application;
using MediatR;
using Shop.Application.Inventories.Create;
using Shop.Application.Inventories.DecreaseQuantity;
using Shop.Application.Inventories.Edit;
using Shop.Application.Inventories.IncreaseQuantity;
using Shop.Application.Inventories.Remove;
using Shop.Application.Inventories.RemoveDiscount;
using Shop.Application.Inventories.SetDiscountPercentage;
using Shop.Query.Inventories._DTOs;
using Shop.Query.Inventories.GetByFilter;
using Shop.Query.Inventories.GetByProductId;

namespace Shop.Presentation.Facade.Inventories;

internal class InventoryFacade : IInventoryFacade
{
    private readonly IMediator _mediator;

    public InventoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> IncreaseQuantity(IncreaseInventoryQuantityCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DecreaseQuantity(DecreaseInventoryQuantityCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetDiscountPercentage(SetInventoryDiscountPercentageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveDiscount(long inventoryId)
    {
        return await _mediator.Send(new RemoveInventoryDiscountCommand(inventoryId));
    }

    public async Task<OperationResult> Remove(long inventoryId)
    {
        return await _mediator.Send(new RemoveInventoryCommand(inventoryId));
    }

    public async Task<InventoryDto?> GetById(long id)
    {
        return await _mediator.Send(new GetInventoryByIdQuery(id));
    }

    public async Task<InventoryFilterResult> GetByFilter(InventoryFilterParams filterParams)
    {
        return await _mediator.Send(new GetInventoryByFilterQuery(filterParams));
    }
}