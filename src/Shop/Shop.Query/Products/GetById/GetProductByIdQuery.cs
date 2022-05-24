using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Categories._Mappers;
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
                .Join(
                    _shopContext.Categories,
                    p => p.CategoryId,
                    c => c.Id,
                    (product, category) => new
                    {
                        Product = product,
                        Category = category
                    })
                .Join(
                    _shopContext.Inventories,
                    tables => tables.Product.Id,
                    i => i.ProductId,
                    (tables, inventory) => new
                    {
                        tables.Product,
                        tables.Category,
                        Inventory = inventory
                    })
                .Join(
                    _shopContext.Colors,
                    tables => tables.Inventory.ColorId,
                    c => c.Id,
                    (tables, color) => new ProductDto
                    {
                        Id = tables.Product.Id,
                        CreationDate = tables.Product.CreationDate,
                        CategoryId = tables.Product.CategoryId,
                        Name = tables.Product.Name,
                        EnglishName = tables.Product.EnglishName,
                        Slug = tables.Product.Slug,
                        Description = tables.Product.Description,
                        Scores = tables.Product.Scores.ToList(),
                        MainImage = tables.Product.MainImage.Name,
                        GalleryImages = tables.Product.GalleryImages.ToList().MapToProductImageDto(),
                        CustomSpecifications = tables.Product.CustomSpecifications.ToList().MapToProductSpecificationDto(),
                        CategorySpecifications = tables.Category.Specifications.ToList().MapToCategorySpecificationDto(),
                        ExtraDescriptions = tables.Product.ExtraDescriptions.ToList().MapToExtraDescriptionDto(),
                        ProductInventories = new List<ProductInventoryDto>
                        {
                            new ProductInventoryDto
                            {
                                Id = tables.Inventory.Id,
                                CreationDate = tables.Inventory.CreationDate,
                                ProductId = tables.Inventory.Id,
                                Quantity = tables.Inventory.Quantity,
                                Price = tables.Inventory.Price.Value,
                                ColorName = color.Name,
                                ColorCode = color.Code,
                                IsAvailable = tables.Inventory.IsAvailable,
                                DiscountPercentage = tables.Inventory.DiscountPercentage,
                                IsDiscounted = tables.Inventory.IsDiscounted
                            }
                        }
                    })
                .ToListAsync(cancellationToken);

        var groupedProduct = productDtos.GroupBy(p => p.Id).Select(productGroup =>
        {
            var firstItem = productGroup.First();
            firstItem.Scores = productGroup.SelectMany(p => p.Scores).ToList();
            firstItem.GalleryImages = productGroup.SelectMany(p => p.GalleryImages).ToList();
            firstItem.CustomSpecifications = productGroup.SelectMany(p => p.CustomSpecifications).ToList();
            firstItem.CategorySpecifications = productGroup.SelectMany(p => p.CategorySpecifications).ToList();
            firstItem.ExtraDescriptions = productGroup.SelectMany(p => p.ExtraDescriptions).ToList();
            firstItem.ProductInventories = productGroup.SelectMany(p => p.ProductInventories).ToList();
            return firstItem;
        }).Single();

        return groupedProduct;
    }
}