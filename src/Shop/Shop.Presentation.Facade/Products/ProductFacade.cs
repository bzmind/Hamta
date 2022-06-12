using Common.Application;
using MediatR;
using Shop.Application.Products.AddScore;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.Remove;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.ReplaceMainImage;
using Shop.Query.Products._DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;

namespace Shop.Presentation.Facade.Products;

internal class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;

    public ProductFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ReplaceMainImage(ReplaceProductMainImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveGalleryImage(RemoveProductGalleryImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddScore(AddProductScoreCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long productId)
    {
        return await _mediator.Send(new RemoveProductCommand(productId));
    }

    public async Task<ProductDto?> GetById(long id)
    {
        return await _mediator.Send(new GetProductByIdQuery(id));
    }

    public async Task<ProductFilterResult> GetByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }
}