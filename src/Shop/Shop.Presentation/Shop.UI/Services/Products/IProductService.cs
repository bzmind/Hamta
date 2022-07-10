using Common.Api;
using Shop.Application.Products.AddScore;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.ReplaceMainImage;
using Shop.Query.Products._DTOs;

namespace Shop.UI.Services.Products;

public interface IProductService
{
    Task<ApiResult> Create(CreateProductCommand model);
    Task<ApiResult> Edit(EditProductCommand model);
    Task<ApiResult> ReplaceMainImage(ReplaceProductMainImageCommand model);
    Task<ApiResult> AddScore(AddProductScoreCommand model);
    Task<ApiResult> RemoveGalleryImage(RemoveProductGalleryImageCommand model);
    Task<ApiResult> Remove(long productId);

    Task<ProductDto> GetById(long productId);
    Task<ProductFilterResult> GetByFilter(ProductFilterParams filterParams);
}