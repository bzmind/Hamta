using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Inventories._DTOs;
using Shop.Query.Inventories._Mappers;

namespace Shop.Query.Inventories.GetByProductId;

public record GetInventoryByIdQuery(long InventoryId) : IBaseQuery<InventoryDto>;

public class GetInventoryByIdQueryHandler : IBaseQueryHandler<GetInventoryByIdQuery, InventoryDto>
{
    private readonly ShopContext _shopContext;

    public GetInventoryByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<InventoryDto> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        var inventory = await _shopContext.Inventories
            .FirstOrDefaultAsync(i => i.Id == request.InventoryId, cancellationToken);

        return inventory.MapToInventoryDto();
    }
}