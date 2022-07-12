using Common.Api;
using Common.Api.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Domain.RoleAggregate;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories._DTOs;
using System.Net;
using AutoMapper;
using Shop.API.ViewModels.Categories;

namespace Shop.API.Controllers;

[CheckPermission(RolePermission.Permissions.CategoryManager)]
public class CategoryController : BaseApiController
{
    private readonly ICategoryFacade _categoryFacade;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryFacade categoryFacade, IMapper mapper)
    {
        _categoryFacade = categoryFacade;
        _mapper = mapper;
    }

    [HttpPost("Create")]
    public async Task<ApiResult<long>> Create(CreateCategoryViewModel model)
    {
        var command = _mapper.Map<CreateCategoryCommand>(model);
        var result = await _categoryFacade.Create(command);
        var resultUrl = Url.Action("Create", "Category", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPost("AddSubCategory")]
    public async Task<ApiResult<long>> AddSubCategory(AddSubCategoryViewModel model)
    {
        var command = _mapper.Map<AddSubCategoryCommand>(model);
        var result = await _categoryFacade.AddSubCategory(command);
        var resultUrl = Url.Action("AddSubCategory", "Category", new { id = result.Data }, Request.Scheme);
        return CommandResult(result, HttpStatusCode.Created, resultUrl);
    }

    [HttpPut("Edit")]
    public async Task<ApiResult> Edit(EditCategoryViewModel model)
    {
        var command = _mapper.Map<EditCategoryCommand>(model);
        var result = await _categoryFacade.Edit(command);
        return CommandResult(result);
    }

    [HttpDelete("Remove/{categoryId}")]
    public async Task<ApiResult> Remove(long categoryId)
    {
        var result = await _categoryFacade.Remove(categoryId);
        return CommandResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<ApiResult<List<CategoryDto>>> GetAll()
    {
        var result = await _categoryFacade.GetAll();
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetById/{id}")]
    public async Task<ApiResult<CategoryDto?>> GetById(long id)
    {
        var result = await _categoryFacade.GetById(id);
        return QueryResult(result);
    }

    [AllowAnonymous]
    [HttpGet("GetByParentId/{parentId}")]
    public async Task<ApiResult<List<CategoryDto>>> GetByParentId(long parentId)
    {
        var result = await _categoryFacade.GetByParentId(parentId);
        return QueryResult(result);
    }
}