using Common.Application;
using MediatR;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.Remove;
using Shop.Query.Categories._DTOs;
using Shop.Query.Categories.GetById;
using Shop.Query.Categories.GetByParentId;
using Shop.Query.Categories.GetList;
using Shop.Query.Categories.Specifications;

namespace Shop.Presentation.Facade.Categories;

internal class CategoryFacade : ICategoryFacade
{
    private readonly IMediator _mediator;

    public CategoryFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult<long>> Create(CreateCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult<long>> AddSubCategory(AddSubCategoryCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Remove(long subCategoryId)
    {
        return await _mediator.Send(new RemoveCategoryCommand(subCategoryId));
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        return await _mediator.Send(new GetCategoryListQuery());
    }

    public async Task<CategoryDto?> GetById(long id)
    {
        return await _mediator.Send(new GetCategoryByIdQuery(id));
    }

    public async Task<List<CategoryDto>> GetByParentId(long parentId)
    {
        return await _mediator.Send(new GetCategoryByParentIdQuery(parentId));
    }

    public async Task<List<QueryCategorySpecificationDto>> GetSpecificationsByCategoryId(long categoryId)
    {
        return await _mediator.Send(new GetCategorySpecificationsByIdQuery(categoryId));
    }
}