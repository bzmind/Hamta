using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Categories._DTOs;
using Shop.Query.Categories._Mappers;

namespace Shop.Query.Categories.GetList;

public record GetCategoryListQuery : IBaseQuery<List<CategoryDto>>;

public class GetCategoryListQueryHandler : IBaseQueryHandler<GetCategoryListQuery, List<CategoryDto>>
{
    private readonly ShopContext _shopContext;

    public GetCategoryListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        return await _shopContext.Categories
            .Where(c => c.ParentId == null)
            .Select(c => c.MapToCategoryDto())
            .OrderByDescending(c => c.Id)
            .ToListAsync(cancellationToken);
    }
}