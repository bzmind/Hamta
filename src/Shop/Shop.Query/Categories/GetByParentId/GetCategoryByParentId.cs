using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Infrastructure.Utility;
using Shop.Query.Categories._DTOs;
using Shop.Query.Categories._Mappers;

namespace Shop.Query.Categories.GetByParentId;

public record GetCategoryByParentIdQuery(long ParentCategoryId) : IBaseQuery<List<CategoryDto>>;

public class GetCategoryByParentIdQueryHandler : IBaseQueryHandler<GetCategoryByParentIdQuery, List<CategoryDto>>
{
    private readonly ShopContext _shopContext;

    public GetCategoryByParentIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var categories = await _shopContext.Categories
            .Where(c => c.Id >= request.ParentCategoryId)
            .OrderBy(c => c.Id)
            .ToListAsync(cancellationToken);

        var parentCategory = categories.First();

        if (parentCategory.Id == request.ParentCategoryId)
        {
            parentCategory.FillSubCategories(categories);
            return parentCategory.SubCategories.ToList().MapToCategoryDto();
        }

        return new List<CategoryDto>();
    }
}