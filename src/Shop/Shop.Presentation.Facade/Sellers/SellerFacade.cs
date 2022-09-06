using Common.Application;
using MediatR;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.Inventories.Add;
using Shop.Application.Sellers.Inventories.DecreaseQuantity;
using Shop.Application.Sellers.Inventories.Edit;
using Shop.Application.Sellers.Inventories.IncreaseQuantity;
using Shop.Application.Sellers.Inventories.Remove;
using Shop.Application.Sellers.Remove;
using Shop.Application.Sellers.SetStatus;
using Shop.Query.Sellers._DTOs;
using Shop.Query.Sellers.GetByFilter;
using Shop.Query.Sellers.GetById;
using Shop.Query.Sellers.Inventories.GetByFilter;
using Shop.Query.Sellers.Inventories.GetById;

namespace Shop.Presentation.Facade.Sellers;

internal class SellerFacade : ISellerFacade
{
    private readonly IMediator _mediator;

    public SellerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateSellerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditSellerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetStatus(SetSellerStatusCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult<long>> AddInventory(AddSellerInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditInventory(EditSellerInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveInventory(RemoveSellerInventoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> IncreaseInventoryQuantity(IncreaseSellerInventoryQuantityCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DecreaseInventoryQuantity(DecreaseSellerInventoryQuantityCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long sellerId)
    {
        return await _mediator.Send(new RemoveSellerCommand(sellerId));
    }

    public async Task<SellerDto?> GetCurrentSeller(long userId)
    {
        return await _mediator.Send(new GetSellerByUserIdQuery(userId));
    }

    public async Task<SellerFilterResult> GetByFilter(SellerFilterParams filterParams)
    {
        return await _mediator.Send(new GetSellersByFilterQuery(filterParams));
    }

    public async Task<SellerInventoryDto?> GetInventoryById(long id)
    {
        return await _mediator.Send(new GetSellerInventoryByIdQuery(id));
    }

    public async Task<SellerInventoryFilterResult> GetInventoriesByFilter(SellerInventoryFilterParams filterParams)
    {
        return await _mediator.Send(new GetSellerInventoriesByFilterQuery(filterParams));
    }
}