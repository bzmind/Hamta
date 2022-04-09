using Common.Application;
using Common.Application.Base_Classes;
using Shop.Domain.Customer_Aggregate;
using Shop.Domain.Customer_Aggregate.Repository;

namespace Shop.Application.Customers.Use_Cases.AddFavoriteItem;

public record AddCustomerFavoriteItemCommand(long CustomerId, long ProductId) : IBaseCommand;

public class AddCustomerFavoriteItemCommandHandler : IBaseCommandHandler<AddCustomerFavoriteItemCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public AddCustomerFavoriteItemCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(AddCustomerFavoriteItemCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        var favoriteItem = new CustomerFavoriteItem(request.CustomerId, request.ProductId);
        customer.AddFavoriteItem(favoriteItem);

        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}