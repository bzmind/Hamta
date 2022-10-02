using Shop.Domain.CategoryAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductMapper
{
    public static ProductDto? MapToProductDto(this Product product)
    {
        if (product == null)
            return null;

        var productDto = new ProductDto
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            CategoryId = product.CategoryId,
            Name = product.Name,
            EnglishName = product.EnglishName,
            Slug = product.Slug,
            Introduction = product.Introduction,
            Review = product.Review,
            MainImage = product.MainImage,
            GalleryImages = product.GalleryImages.ToList().MapToProductImageDto(),
            CategorySpecifications = product.CategorySpecifications.ToList()
                .MapToProductCategorySpecificationQueryDto(),
            Specifications = product.Specifications.ToList().MapToQueryProductSpecificationDto()
        };
        return productDto;
    }

    public static List<ProductCategorySpecificationQueryDto> MapToProductCategorySpecificationQueryDto(
        this List<CategorySpecification> categorySpecifications,
        List<ProductCategorySpecificationQueryDto> productCategorySpecifications)
    {
        var productCategorySpecificationsDtos = new List<ProductCategorySpecificationQueryDto>();
        productCategorySpecifications.ForEach(productCategorySpec =>
        {
            var categorySpec = categorySpecifications
                .FirstOrDefault(s => s.Id == productCategorySpec.CategorySpecificationId);
            productCategorySpecificationsDtos.Add(new ProductCategorySpecificationQueryDto
            {
                Id = productCategorySpec.Id,
                CreationDate = categorySpec.CreationDate,
                CategorySpecificationId = categorySpec.Id,
                ProductId = productCategorySpec.ProductId,
                Title = categorySpec.Title,
                Description = productCategorySpec.Description,
                IsImportant = categorySpec.IsImportant,
                IsOptional = categorySpec.IsOptional
            });
        });
        return productCategorySpecificationsDtos;
    }
}