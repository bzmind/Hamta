using Common.Application;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Presentation.Facade.Caching;
using Shop.Query.Categories._DTOs;
using Shop.Query.Categories.GetById;
using Shop.Query.Categories.GetByParentId;
using Shop.Query.Categories.GetList;
using Shop.Query.Categories.Specifications;

namespace Shop.Presentation.Facade.Categories;

internal class CategoryFacade : ICategoryFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _cache;

    public CategoryFacade(IMediator mediator, IDistributedCache cache)
    {
        _mediator = mediator;
        _cache = cache;
    }

    public async Task<OperationResult<long>> Create(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCategoryCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.Categories);
        return await _mediator.Send(command);
    }

    public async Task<OperationResult<long>> AddSubCategory(AddSubCategoryCommand command)
    {
        await _cache.RemoveAsync(CacheKeys.Categories);
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long subCategoryId)
    {
        await _cache.RemoveAsync(CacheKeys.Categories);
        return await _mediator.Send(new RemoveCategoryCommand(subCategoryId));
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        return await _cache.GetOrSet(CacheKeys.Categories,
            async () => await _mediator.Send(new GetCategoryListQuery()));
    }

    public async Task<List<CategoryDto>> GetForMenu()
    {
        return await _cache.GetOrSet(CacheKeys.MenuCategories,
            async () => await _mediator.Send(new GetMenuCategoryListQuery()));
    }

    public async Task<CategoryDto?> GetById(long id)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(id));
    }

    public async Task<List<CategoryDto>> GetByParentId(long parentId)
    {
        return await _mediator.Send(new GetCategoryByParentIdQuery(parentId));
    }

    public async Task<List<CategorySpecificationQueryDto>> GetSpecificationsByCategoryId(long categoryId)
    {
        return await _mediator.Send(new GetCategorySpecificationsByIdQuery(categoryId));
    }
}