using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Infrastructure.Utility;
using Shop.Query.Categories._DTOs;
using Shop.Query.Categories._Mappers;

namespace Shop.Query.Categories.GetById;

public record GetCategoryByIdQuery(long CategoryId) : IBaseQuery<CategoryDto?>;

public class GetCategoryByIdQueryHandler : IBaseQueryHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly ShopContext _shopContext;

    public GetCategoryByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categories = await _shopContext.Categories
            .Where(c => c.Id >= request.CategoryId)
            .OrderBy(c => c.Id)
            .ToListAsync(cancellationToken);

        var category = categories.First();

        if (category.Id == request.CategoryId)
        {
            category.FillSubCategories(categories);
            return category.MapToCategoryDto();
        }

        return null;
    }
}