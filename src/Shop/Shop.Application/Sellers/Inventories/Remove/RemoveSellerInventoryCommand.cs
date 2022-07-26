using Common.Application;
using Common.Application.BaseClasses;
using Common.Application.Utility.Validation;
using Shop.Domain.SellerAggregate.Repository;

namespace Shop.Application.Sellers.Inventories.Remove;

public record RemoveSellerInventoryCommand(long UserId, long InventoryId) : IBaseCommand;

public class RemoveSellerInventoryCommandHandler : IBaseCommandHandler<RemoveSellerInventoryCommand>
{
    private readonly ISellerRepository _sellerRepository;

    public RemoveSellerInventoryCommandHandler(ISellerRepository sellerRepository)
    {
        _sellerRepository = sellerRepository;
    }

    public async Task<OperationResult> Handle(RemoveSellerInventoryCommand request, CancellationToken cancellationToken)
    {
        var seller = await _sellerRepository.GetSellerByUserIdAsTrackingAsync(request.UserId);
        if (seller == null)
            return OperationResult.NotFound(ValidationMessages.FieldNotFound("فروشنده"));

        seller.RemoveInventory(request.InventoryId);
        await _sellerRepository.SaveAsync();
        return OperationResult.Success();
    }
}