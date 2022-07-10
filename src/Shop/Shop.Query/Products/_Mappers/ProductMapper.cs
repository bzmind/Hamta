﻿using Shop.Domain.CategoryAggregate;
using Shop.Domain.ColorAggregate;
using Shop.Domain.InventoryAggregate;
using Shop.Domain.ProductAggregate;
using Shop.Query.Categories._Mappers;
using Shop.Query.Products._DTOs;

namespace Shop.Query.Products._Mappers;

internal static class ProductMapper
{
    public static ProductDto? MapToProductDto(this Product product, Category category, Inventory inventory,
        Color color)
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
            AverageScore = product.AverageScore,
            MainImage = product.MainImage.Name,
            GalleryImages = product.GalleryImages.ToList().MapToProductImageDto(),
            CustomSpecifications = product.CustomSpecifications.ToList().MapToProductSpecificationDto(),
            CategorySpecifications = category.Specifications.ToList().MapToCategorySpecificationDto(),
            ExtraDescriptions = product.ExtraDescriptions.ToList().MapToExtraDescriptionDto(),
            ProductInventories = new List<ProductInventoryDto>
            {
                new ProductInventoryDto()
                {
                    Id = inventory.Id,
                    CreationDate = inventory.CreationDate,
                    ProductId = inventory.Id,
                    Quantity = inventory.Quantity,
                    Price = inventory.Price.Value,
                    ColorName = color.Name,
                    ColorCode = color.Code,
                    IsAvailable = inventory.IsAvailable,
                    DiscountPercentage = inventory.DiscountPercentage,
                    IsDiscounted = inventory.IsDiscounted
                }
            }
        };
    }
}