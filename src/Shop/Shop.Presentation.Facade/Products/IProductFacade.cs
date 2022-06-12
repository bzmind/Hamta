﻿using Common.Application;
using Shop.Application.Products.AddScore;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.ReplaceMainImage;
using Shop.Query.Products._DTOs;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{
    Task<OperationResult<long>> Create(CreateProductCommand command);
    Task<OperationResult> Edit(EditProductCommand command);
    Task<OperationResult> ReplaceMainImage(ReplaceProductMainImageCommand command);
    Task<OperationResult> RemoveGalleryImage(RemoveProductGalleryImageCommand command);
    Task<OperationResult> AddScore(AddProductScoreCommand command);
    Task<OperationResult> Remove(long productId);

    Task<ProductDto?> GetById(long id);
    Task<ProductFilterResult> GetByFilter(ProductFilterParams filterParams);
}