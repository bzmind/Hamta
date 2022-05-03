using Common.Application;
using MediatR;
using Shop.Application.Products.Create;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.SetScore;
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

    public async Task<OperationResult> Create(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RemoveGalleryImage(RemoveGalleryImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> SetScore(SetScoreCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ProductDto?> GetProductById(long id)
    {
        return await _mediator.Send(new GetProductByIdQuery(id));
    }

    public async Task<ProductFilterResult> GetProductByFilter(ProductFilterParam filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }
}