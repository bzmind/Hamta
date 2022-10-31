using Common.Api;
using Shop.API.ViewModels.Products;
using Shop.Query.Products._DTOs;

namespace Shop.UI.Services.Products;

public interface IProductService
{
    Task<ApiResult> Create(CreateProductViewModel model);
    Task<ApiResult> Edit(EditProductViewModel model);
    Task<ApiResult> AddScore(AddProductScoreViewModel model);
    Task<ApiResult> Remove(long productId);
    Task<ApiResult<string?>> AddReviewImage(AddProductReviewImageViewModel model);

    Task<ApiResult<ProductFilterResult>> GetByFilter(ProductFilterParams filterParams);
    Task<ApiResult<ProductForShopResult>> GetForShopByFilter(ProductForShopFilterParams filterFilterParams);
    Task<ApiResult<ProductDto?>> GetById(long productId);
    Task<ApiResult<SingleProductDto?>> GetSingleBySlug(string slug);
}