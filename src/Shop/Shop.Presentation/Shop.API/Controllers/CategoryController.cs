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

    [HttpDelete("{subCategoryId}")]
    public async Task<ApiResult> Remove(long subCategoryId)
    {
        var result = await _categoryFacade.RemoveCategory(subCategoryId);
        return CommandResult(result);
    }

    [HttpGet]
    public async Task<ApiResult<List<CategoryDto>>> GetCategories()
    {
        var result = await _categoryFacade.GetCategoryList();
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<CategoryDto?>> GetCategoryById(long id)
    {
        var result = await _categoryFacade.GetCategoryById(id);
        return QueryResult(result);
    }

    [HttpGet("getSubCategories/{parentId}")]
    public async Task<ApiResult<List<CategoryDto>>> GetCategoryByParentId(long parentId)
    {
        var result = await _categoryFacade.GetCategoryByParentId(parentId);
        return QueryResult(result);
    }
}