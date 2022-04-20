using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.ShippingAggregate.Repository;

namespace Shop.Application.Shippings.Remove;

public record RemoveShippingCommand(long ShippingId) : IBaseCommand;

public class RemoveShippingCommandHandler : IBaseCommandHandler<RemoveShippingCommand>
{
    private readonly IShippingRepository _shippingRepository;

    public RemoveShippingCommandHandler(IShippingRepository shippingRepository)
    {
        _shippingRepository = shippingRepository;
    }

    public async Task<OperationResult> Handle(RemoveShippingCommand request, CancellationToken cancellationToken)
    {
        var shipping = await _shippingRepository.GetAsync(request.ShippingId);

        if (shipping == null)
            return OperationResult.NotFound();

        _shippingRepository.Delete(shipping);
        await _shippingRepository.SaveAsync();
        return OperationResult.Success();
    }
}