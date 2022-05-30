using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories._DTOs;

namespace Shop.API.Controllers;

public class CategoryController : BaseApiController
{
    private readonly ICategoryFacade _categoryFacade;

    public CategoryController(ICategoryFacade categoryFacade)
    {
        _categoryFacade = categoryFacade;
    }

    [HttpPost]
    public async Task<ApiResult<long>> Create(CreateCategoryCommand command)
    {
        var model = new CreateCategoryCommand(command.Title, command.Slug, command.Specifications);
        var result = await _categoryFacade.Create(model);
        var resultUrl = Url.Action("Create", "Category", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut]
    public async Task<ApiResult> Edit(EditCategoryCommand command)
    {
        var result = await _categoryFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpPost("AddSubCategory")]
    public async Task<ApiResult<long>> AddSubCategory(AddSubCategoryCommand command)
    {
        var result = await _categoryFacade.AddSubCategory(command);
        var resultUrl = Url.Action("AddSubCategory", "Category", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpDelete("{categoryId}")]
    public async Task<ApiResult> Remove(long categoryId)
    {
        var result = await _categoryFacade.Remove(categoryId);
        return CommandResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<List<CategoryDto>>> GetAll()
    {
        var result = await _categoryFacade.GetAll();
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<CategoryDto?>> GetById(long id)
    {
        var result = await _categoryFacade.GetById(id);
        return QueryResult(result);
    }

    [HttpGet("GetSubCategories/{parentId}")]
    public async Task<ApiResult<List<CategoryDto>>> GetByParentId(long parentId)
    {
        var result = await _categoryFacade.GetByParentId(parentId);
        return QueryResult(result);
    }
}