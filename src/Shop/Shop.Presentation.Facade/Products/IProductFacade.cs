using Common.Application;
using Shop.Application.Products;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Query.Products._DTOs;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{
    Task<OperationResult<long>> Create(CreateProductCommand command);
    Task<OperationResult> Edit(EditProductCommand command);
    Task<OperationResult> Remove(long productId);
    Task<OperationResult<string>> AddReviewImage(AddProductReviewImageCommand command);

    Task<ProductFilterResult> GetByFilter(ProductFilterParams filterParams);
    Task<ProductForShopResult> GetForShopByFilter(ProductForShopFilterParams filterFilterParams);
    Task<ProductDto?> GetById(long id);
    Task<SingleProductDto?> GetSingleBySlug(string slug);
}