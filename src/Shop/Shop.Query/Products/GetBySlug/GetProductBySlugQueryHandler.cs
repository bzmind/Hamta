using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Products._DTOs;
using Shop.Query.Products._Mappers;

namespace Shop.Query.Products.GetBySlug;

public record GetProductBySlugQuery(string Slug) : IBaseQuery<ProductDto?>;

public class GetProductBySlugQueryHandler : IBaseQueryHandler<GetProductBySlugQuery, ProductDto?>
{
    private readonly ShopContext _shopContext;

    public GetProductBySlugQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<ProductDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var productDtos =
            await _shopContext.Products
                .Where(product => product.Slug == request.Slug)
                .GroupJoin(
                    _shopContext.Categories,
                    product => product.CategoryId,
                    category => category.Id,
                    (product, categories) => new
                    { Product = product, Categories = categories })
                .SelectMany(
                    tables => tables.Categories.DefaultIfEmpty(),
                    (tables, category) => new
                    { tables.Product, Category = category })
                .GroupJoin(
                    _shopContext.Sellers.SelectMany(seller => seller.Inventories),
                    tables => tables.Product.Id,
                    inventory => inventory.ProductId,
                    (tables, inventories) => new
                    { tables.Product, tables.Category, Inventories = inventories })
                .SelectMany(
                    tables => tables.Inventories.DefaultIfEmpty(),
                    (tables, inventory) => new
                    { tables.Product, tables.Category, Inventory = inventory })
                .GroupJoin(
                    _shopContext.Colors,
                    tables => tables.Inventory.ColorId,
                    color => color.Id,
                    (tables, colors) => new
                    { tables.Product, tables.Inventory, tables.Category, Colors = colors })
                .SelectMany(
                    tables => tables.Colors.DefaultIfEmpty(),
                    (tables, color) => new
                    { tables.Product, tables.Inventory, tables.Category, Color = color })
                .ToListAsync(cancellationToken);

        var groupedProduct = productDtos
            .Select(t => t.Product.MapToProductDto(t.Category, t.Inventory, t.Color))
            .GroupBy(product => product.Id).Select(grouping =>
            {
                var firstItem = grouping.First();
                firstItem.GalleryImages = grouping
                    .Select(p => p.GalleryImages.OrderBy(gi => gi.Sequence).ToList()).First();
                firstItem.Specifications = grouping.Select(p => p.Specifications).First();
                firstItem.CategorySpecifications = grouping.Select(p => p.CategorySpecifications).First();
                firstItem.Inventories = grouping.Select(p => p.Inventories).First();
                return firstItem;
            }).Single();

        return groupedProduct;
    }
}