using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.Mappers;

namespace Shop.Query.Categories.GetById;

public record GetCategoryByIdQuery(long CategoryId) : IBaseQuery<CategoryDto>;

public class GetCategoryByIdQueryHandler : IBaseQueryHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly ShopContext _shopContext;

    public GetCategoryByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _shopContext.Categories
            .FirstOrDefaultAsync(c => c.Id == request.CategoryId, cancellationToken);

        return model.MapToCategoryDto();
    }
}