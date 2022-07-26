using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Sellers.Remove;

public record RemoveSellerCommand(long UserId) : IBaseCommand;

public class RemoveSellerHandler : IBaseCommandHandler<RemoveSellerCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public RemoveSellerHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(RemoveSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetSellerByUserIdAsTrackingAsync(request.UserId);
        if (seller == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("فروشنده"));

        _sellerRepository.Delete(seller);

        await _sellerRepository.SaveAsync();
        return OperationResult.Success();
    }
}