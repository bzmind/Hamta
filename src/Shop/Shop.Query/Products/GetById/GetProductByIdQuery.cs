using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Products._DTOs;
using Shop.Query.Products._Mappers;

namespace Shop.Query.Products.GetById;

public record GetProductByIdQuery(long ProductId) : IBaseQuery<ProductDto?>;

public class GetProductByIdQueryHandler : IBaseQueryHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly ShopContext _shopContext;
    private readonly DapperContext _dapperContext;

    public GetProductByIdQueryHandler(ShopContext shopContext, DapperContext dapperContext)
    {
        _shopContext = shopContext;
        _dapperContext = dapperContext;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productDto =
            await _shopContext.Products
                .Join(
                    _shopContext.Categories,
                    p => p.CategoryId,
                    c => c.Id,
                    (product, category) => product.MapToProductDto(category)
                    )
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

        productDto.ProductInventories = await productDto.GetProductInventoriesAsDto(_dapperContext);
        return productDto;
    }
}