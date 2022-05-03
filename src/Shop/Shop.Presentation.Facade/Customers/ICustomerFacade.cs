using Common.Application;
using Shop.Application.Customers.AddFavoriteItem;
using Shop.Application.Customers.Create;
using Shop.Application.Customers.Edit;
using Shop.Application.Customers.RemoveFavoriteItem;
using Shop.Application.Customers.SetAvatar;
using Shop.Application.Customers.SetSubscriptionToNews;
using Shop.Query.Customers._DTOs;

namespace Shop.Presentation.Facade.Customers;

public interface ICustomerFacade
{
    Task<OperationResult> Create(CreateCustomerCommand command);
    Task<OperationResult> Edit(EditCustomerCommand command);
    Task<OperationResult> SetAvatar(SetCustomerAvatarCommand command);
    Task<OperationResult> SetSubscriptionToNews(SetCustomerSubscriptionToNewsCommand command);
    Task<OperationResult> AddFavoriteItem(AddCustomerFavoriteItemCommand command);
    Task<OperationResult> RemoveFavoriteItem(RemoveCustomerFavoriteItemCommand command);

    Task<CustomerDto?> GetCustomerById(long id);
    Task<CustomerDto?> GetCustomerByPhoneNumber(string phoneNumber);
    Task<CustomerFilterResult> GetCustomerByFilter(CustomerFilterParam filterParams);
}