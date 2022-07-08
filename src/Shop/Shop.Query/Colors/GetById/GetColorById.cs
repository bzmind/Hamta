using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Colors._DTOs;
using Shop.Query.Colors._Mappers;

namespace Shop.Query.Colors.GetById;

public record GetColorByIdQuery(long ColorId) : IBaseQuery<ColorDto?>;

public class GetColorByIdQueryHandler : IBaseQueryHandler<GetColorByIdQuery, ColorDto?>
{
    private readonly ShopContext _shopContext;

    public GetColorByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<ColorDto?> Handle(GetColorByIdQuery request, CancellationToken cancellationToken)
    {
        var color = await _shopContext.Colors.FirstOrDefaultAsync(c => c.Id == request.ColorId, cancellationToken);
        return color.MapToColorDto();
    }
}