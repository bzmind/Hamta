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

    public GetProductByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productDtos =
            await _shopContext.Products
                .Where(product => product.Id == request.ProductId)
                .Join(
                    _shopContext.Categories,
                    product => product.CategoryId,
                    category => category.Id,
                    (product, category) => new
                    {
                        Product = product,
                        Category = category
                    })
                .Join(
                    _shopContext.Sellers.SelectMany(seller => seller.Inventories),
                    tables => tables.Product.Id,
                    inventory => inventory.ProductId,
                    (tables, inventory) => new
                    {
                        tables.Product,
                        tables.Category,
                        Inventory = inventory
                    })
                .Join(
                    _shopContext.Colors,
                    tables => tables.Inventory.ColorId,
                    color => color.Id,
                    (tables, color) => 
                        tables.Product.MapToProductDto(tables.Category, tables.Inventory, color))
                .ToListAsync(cancellationToken);

        var groupedProduct = productDtos.GroupBy(productDto => productDto.Id).Select(productGroup =>
        {
            var firstItem = productGroup.First();
            firstItem.GalleryImages = productGroup.Select(p => p.GalleryImages).First();
            firstItem.CustomSpecifications = productGroup.Select(p => p.CustomSpecifications).First();
            firstItem.CategorySpecifications = productGroup.Select(p => p.CategorySpecifications).First();
            firstItem.ExtraDescriptions = productGroup.Select(p => p.ExtraDescriptions).First();
            firstItem.ProductInventories = productGroup.Select(p => p.ProductInventories).First();
            return firstItem;
        }).Single();

        return groupedProduct;
    }
}