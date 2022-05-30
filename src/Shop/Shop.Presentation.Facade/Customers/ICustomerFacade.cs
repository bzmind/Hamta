using Common.Application;
using Shop.Application.Customers.AddFavoriteItem;
using Shop.Application.Customers.Create;
using Shop.Application.Customers.Edit;
using Shop.Application.Customers.Remove;
using Shop.Application.Customers.RemoveFavoriteItem;
using Shop.Application.Customers.SetAvatar;
using Shop.Application.Customers.SetSubscriptionToNews;
using Shop.Query.Customers._DTOs;

namespace Shop.Presentation.Facade.Customers;

public interface ICustomerFacade
{
    Task<OperationResult<long>> Create(CreateCustomerCommand command);
    Task<OperationResult> Edit(EditCustomerCommand command);
    Task<OperationResult> SetAvatar(SetCustomerAvatarCommand command);
    Task<OperationResult> SetSubscriptionToNews(SetCustomerSubscriptionToNewsCommand command);
    Task<OperationResult> AddFavoriteItem(AddCustomerFavoriteItemCommand command);
    Task<OperationResult> RemoveFavoriteItem(RemoveCustomerFavoriteItemCommand command);
    Task<OperationResult> Remove(long customerId);

    Task<CustomerDto?> GetById(long id);
    Task<CustomerDto?> GetByPhoneNumber(string phoneNumber);
    Task<CustomerFilterResult> GetByFilter(CustomerFilterParam filterParams);
}