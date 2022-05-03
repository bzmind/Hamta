using Common.Application;
using MediatR;
using Shop.Application.Customers.AddFavoriteItem;
using Shop.Application.Customers.Create;
using Shop.Application.Customers.Edit;
using Shop.Application.Customers.RemoveFavoriteItem;
using Shop.Application.Customers.SetAvatar;
using Shop.Application.Customers.SetSubscriptionToNews;
using Shop.Query.Customers._DTOs;
using Shop.Query.Customers.GetByFilter;
using Shop.Query.Customers.GetById;
using Shop.Query.Customers.GetByPhoneNumber;

namespace Shop.Presentation.Facade.Customers;

internal class CustomerFacade : ICustomerFacade
{
    private readonly IMediator _mediator;

    public CustomerFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateCustomerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCustomerCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetAvatar(SetCustomerAvatarCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetSubscriptionToNews(SetCustomerSubscriptionToNewsCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddFavoriteItem(AddCustomerFavoriteItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveFavoriteItem(RemoveCustomerFavoriteItemCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<CustomerDto?> GetCustomerById(long id)
    {
        return await _mediator.Send(new GetCustomerByIdQuery(id));
    }

    public async Task<CustomerDto?> GetCustomerByPhoneNumber(string phoneNumber)
    {
        return await _mediator.Send(new GetCustomerByPhoneNumberQuery(phoneNumber));
    }

    public async Task<CustomerFilterResult> GetCustomerByFilter(CustomerFilterParam filterParams)
    {
        return await _mediator.Send(new GetCustomerByFilterQuery(filterParams));
    }
}