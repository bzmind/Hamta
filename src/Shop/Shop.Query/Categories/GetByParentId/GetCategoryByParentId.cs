using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
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
            .Where(c => c.ParentId == request.ParentCategoryId)
            .ToListAsync(cancellationToken);

        return categories.MapToCategoryDto();
    }
}