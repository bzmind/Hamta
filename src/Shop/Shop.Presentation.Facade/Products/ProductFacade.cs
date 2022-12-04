using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Products;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.Remove;
using Shop.Presentation.Facade.Caching;
using Shop.Query.Products._DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetForShopByFilter;

namespace Shop.Presentation.Facade.Products;

internal class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public ProductFacade(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<OperationResult<long>> Create(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditProductCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.Product(command.Slug));
        await _cache.RemoveAsync(CacheKeys.Product(command.ProductId));
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long productId)
    {
        return await _mediator.Send(new RemoveProductCommand(productId));
    }

    public async Task<OperationResult<string>> AddReviewImage(AddProductReviewImageCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<ProductFilterResult> GetByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }

    public async Task<ProductForShopResult> GetForShopByFilter(ProductForShopFilterParams filterFilterParams)
    {
        return await _mediator.Send(new GetProductForShopByFilterQuery(filterFilterParams));
    }

    public async Task<ProductDto?> GetById(long id)
    {
        return await _cache.GetOrSet(CacheKeys.Product(id),
            async () => await _mediator.Send(new GetProductByIdQuery(id)));
    }

    public async Task<SingleProductDto?> GetSingleBySlug(string slug)
    {
        return await _cache.GetOrSet(CacheKeys.Product(slug),
            async () => await _mediator.Send(new GetSingleProductBySlugQuery(slug)));
    }
}