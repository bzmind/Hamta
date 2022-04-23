using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.Mappers;

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
        var model = await _shopContext.Categories
            .Where(c => c.ParentId == null)
            .Include(c => c.SubCategories)
            .ThenInclude(c => c.SubCategories)
            .OrderByDescending(c => c.Id)
            .ToListAsync(cancellationToken);

        return model.MapToCategoryDto();
    }
}