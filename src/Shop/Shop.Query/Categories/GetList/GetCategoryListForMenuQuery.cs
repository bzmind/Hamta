using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Infrastructure.Utility;
using Shop.Query.Categories._DTOs;
using Shop.Query.Categories._Mappers;

namespace Shop.Query.Categories.GetList;

public record GetCategoryListForMenuQuery : IBaseQuery<List<CategoryDto>>;

public class GetCategoryListForMenuQueryHandler : IBaseQueryHandler<GetCategoryListForMenuQuery, List<CategoryDto>>
{
    private readonly ShopContext _shopContext;

    public GetCategoryListForMenuQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryListForMenuQuery request, CancellationToken cancellationToken)
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