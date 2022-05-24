using Common.Application;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.ReplaceMainImage;
using Shop.Application.Products.SetScore;
using Shop.Query.Products._DTOs;

namespace Shop.Presentation.Facade.Products;

public interface IProductFacade
{
    Task<OperationResult<long>> Create(CreateProductCommand command);
    Task<OperationResult> Edit(EditProductCommand command);
    Task<OperationResult> ReplaceMainImage(ReplaceMainImageCommand command);
    Task<OperationResult> RemoveGalleryImage(RemoveGalleryImageCommand command);
    Task<OperationResult> AddScore(AddScoreCommand command);
    Task<OperationResult> Remove(long productId);

    Task<ProductDto?> GetProductById(long id);
    Task<ProductFilterResult> GetProductByFilter(ProductFilterParam filterParams);
}