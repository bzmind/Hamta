using Common.Application;
using Common.Application.Base_Classes;
using Shop.Domain.Customer_Aggregate.Repository;

namespace Shop.Application.Customers.Use_Cases.SetSubscriptionToNews;

public record SetCustomerSubscriptionToNewsCommand(long CustomerId, bool Subscription) : IBaseCommand;

public class SetCustomerSubscriptionToNewsCommandHandler
    : IBaseCommandHandler<SetCustomerSubscriptionToNewsCommand>
{
    private readonly ICustomerRepository _customerRepository;

    public SetCustomerSubscriptionToNewsCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<OperationResult> Handle(SetCustomerSubscriptionToNewsCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsTrackingAsync(request.CustomerId);

        if (customer == null)
            return OperationResult.NotFound();

        customer.SetSubscriptionToNews(request.Subscription);
        await _customerRepository.SaveAsync();
        return OperationResult.Success();
    }
}