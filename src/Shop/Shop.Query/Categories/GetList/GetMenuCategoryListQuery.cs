using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Infrastructure.Utility;
using Shop.Query.Categories._DTOs;
using Shop.Query.Categories._Mappers;

namespace Shop.Query.Categories.GetList;

public record GetMenuCategoryListQuery : IBaseQuery<List<CategoryDto>>;

public class GetMenuCategoryListQueryHandler : IBaseQueryHandler<GetMenuCategoryListQuery, List<CategoryDto>>
{
    private readonly ShopContext _shopContext;

    public GetMenuCategoryListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<CategoryDto>> Handle(GetMenuCategoryListQuery request, CancellationToken cancellationToken)
    {
        var categories = await _shopContext.Categories
            .Where(c => c.ShowInMenu)
            .OrderBy(c => c.Id)
            .ToListAsync(cancellationToken);

        var mainCategories = categories.Where(c => c.ParentId == null).ToList();
        mainCategories.ForEach(c => c.FillSubCategories(categories));

        return mainCategories.MapToCategoryDto();
    }
}