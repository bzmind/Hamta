using Common.Api;
using Shop.Query.Products._DTOs;
using Shop.UI.Models.Products;

namespace Shop.UI.Services.Products;

public interface IProductService
{
    Task<ApiResult?> Create(CreateProductViewModel model);
    Task<ApiResult?> Edit(EditProductViewModel model);
    Task<ApiResult?> ReplaceMainImage(ReplaceMainImageViewModel model);
    Task<ApiResult?> AddScore(AddScoreViewModel model);
    Task<ApiResult?> RemoveGalleryImage(RemoveGalleryImageViewModel model);
    Task<ApiResult?> Remove(long productId);

    Task<ProductDto?> GetById(long productId);
    Task<ProductFilterResult?> GetByFilter(ProductFilterParamsViewModel filterParams);
}