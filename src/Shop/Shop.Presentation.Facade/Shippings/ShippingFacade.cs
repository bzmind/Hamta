using Common.Application;
using MediatR;
using Shop.Application.Shippings.Create;
using Shop.Application.Shippings.Edit;
using Shop.Application.Shippings.Remove;
using Shop.Query.Shippings._DTOs;
using Shop.Query.Shippings.GetById;
using Shop.Query.Shippings.GetList;

namespace Shop.Presentation.Facade.Shippings;

internal class ShippingFacade : IShippingFacade
{
    private readonly IMediator _mediator;

    public ShippingFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateShippingCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditShippingCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(RemoveShippingCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ShippingDto?> GetShippingById(long id)
    {
        return await _mediator.Send(new GetShippingByIdQuery(id));
    }

    public async Task<List<ShippingDto>> GetShippingList()
    {
        return await _mediator.Send(new GetShippingListQuery());
    }
}