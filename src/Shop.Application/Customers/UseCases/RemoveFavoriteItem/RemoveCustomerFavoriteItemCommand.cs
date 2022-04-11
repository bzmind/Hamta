using Common.Application;
using Common.Application.Base_Classes;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.Use_Cases.RemoveFavoriteItem;

public record RemoveCustomerFavoriteItemCommand(long CustomerId, long FavoriteItemId) : IBaseCommand;

public class RemoveCustomerFavoriteItemCommandHandler : IBaseCommandHandler<RemoveCustomerFavoriteItemCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public RemoveCustomerFavoriteItemCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(RemoveCustomerFavoriteItemCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        customer.RemoveFavoriteItem(request.FavoriteItemId);
        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}