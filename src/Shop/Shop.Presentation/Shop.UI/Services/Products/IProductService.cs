using Common.Api;
using Shop.Query.Products._DTOs;
using Shop.UI.Models.Products;

namespace Shop.UI.Services.Products;

public interface IProductService
{
    Task<ApiResult?> Create(CreateProductCommandViewModel model);
    Task<ApiResult?> Edit(EditProductCommandViewModel model);
    Task<ApiResult?> ReplaceMainImage(ReplaceProductMainImageCommandViewModel model);
    Task<ApiResult?> AddScore(AddProductScoreCommandViewModel model);
    Task<ApiResult?> RemoveGalleryImage(RemoveProductGalleryImageCommandViewModel model);
    Task<ApiResult?> Remove(long productId);

    Task<ProductDto?> GetById(long productId);
    Task<ProductFilterResult?> GetByFilter(ProductFilterParamsViewModel filterParams);
}