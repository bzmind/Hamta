using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.CustomerAggregate.Repository;

namespace Shop.Application.Customers.UseCases.SetSubscriptionToNews;

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