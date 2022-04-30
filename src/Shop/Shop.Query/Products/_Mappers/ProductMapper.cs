using Shop.Domain.CategoryAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Query.Categories._Mappers;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductMapper
{
    public static ProductDto MapToProductDto(this Product? product)
    {
        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            CategoryId = product.CategoryId,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            Description = product.Description,
            Scores = product.Scores.ToList(),
            MainImage = product.MainImage.Name,
            GalleryImages = product.GalleryImages.ToList().MapToProductImageDto(),
            CustomSpecifications = product.CustomSpecifications.ToList().MapToSpecificationDto(),
            ExtraDescriptions = product.ExtraDescriptions.ToList().MapToExtraDescriptionDto(),
            ProductInventories = new List<ProductInventoryDto>()
        };
    }

    public static List<ProductDto> MapToProductDto(this List<Product> products)
    {
        var dtoProducts = new List<ProductDto>();

        products.ForEach(product =>
        {
            dtoProducts.Add(new ProductDto
            {
                Id = product.Id,
                CreationDate = product.CreationDate,
                CategoryId = product.CategoryId,
                Name = product.Name,
                EnglishName = product.EnglishName,
                Slug = product.Slug,
                Description = product.Description,
                Scores = product.Scores.ToList(),
                MainImage = product.MainImage.Name,
                GalleryImages = product.GalleryImages.ToList().MapToProductImageDto(),
                CustomSpecifications = product.CustomSpecifications.ToList().MapToSpecificationDto(),
                ExtraDescriptions = product.ExtraDescriptions.ToList().MapToExtraDescriptionDto(),
                ProductInventories = new List<ProductInventoryDto>()
            });
        });

        return dtoProducts;
    }

    public static ProductDto MapToProductDto(this Product? product, Category category)
    {
        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            CategoryId = product.CategoryId,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            Description = product.Description,
            Scores = product.Scores.ToList(),
            MainImage = product.MainImage.Name,
            GalleryImages = product.GalleryImages.ToList().MapToProductImageDto(),
            CustomSpecifications = product.CustomSpecifications.ToList().MapToSpecificationDto(),
            CategorySpecifications = category.Specifications.ToList().MapToSpecificationDto(),
            ExtraDescriptions = product.ExtraDescriptions.ToList().MapToExtraDescriptionDto(),
            ProductInventories = new List<ProductInventoryDto>()
        };
    }

    public static List<ProductDto> MapToProductDto(this List<Product> products, Category category)
    {
        var dtoProducts = new List<ProductDto>();

        products.ForEach(product =>
        {
            dtoProducts.Add(new ProductDto
            {
                Id = product.Id,
                CreationDate = product.CreationDate,
                CategoryId = product.CategoryId,
                Name = product.Name,
                EnglishName = product.EnglishName,
                Slug = product.Slug,
                Description = product.Description,
                Scores = product.Scores.ToList(),
                MainImage = product.MainImage.Name,
                GalleryImages = product.GalleryImages.ToList().MapToProductImageDto(),
                CustomSpecifications = product.CustomSpecifications.ToList().MapToSpecificationDto(),
                CategorySpecifications = category.Specifications.ToList().MapToSpecificationDto(),
                ExtraDescriptions = product.ExtraDescriptions.ToList().MapToExtraDescriptionDto(),
                ProductInventories = new List<ProductInventoryDto>()
            });
        });

        return dtoProducts;
    }
}