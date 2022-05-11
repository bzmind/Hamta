using System.Net;
using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Presentation.Facade.Categories;
using Shop.Query.Categories._DTOs;

namespace Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoryController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
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

        [HttpPost]
        public async Task<ApiResult<long>> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _categoryFacade.Create(command);
            var resultUrl = Url.Action("CreateCategory", "Category", new {id = result.Data}, Request.Scheme);
            return CommandResult(result, HttpStatusCode.Created, resultUrl);
        }

        [HttpPost("AddSubCategory")]
        public async Task<ApiResult<long>> AddSubCategory(AddSubCategoryCommand command)
        {
            var result = await _categoryFacade.AddSubCategory(command);
            var resultUrl = Url.Action("AddSubCategory", "Category", new { id = result.Data }, Request.Scheme);
            return CommandResult(result, HttpStatusCode.Created, resultUrl);
        }

        [HttpPut]
        public async Task<ApiResult> EditCategory(EditCategoryCommand command)
        {
            var result = await _categoryFacade.Edit(command);
            return CommandResult(result);
        }

        [HttpDelete("{subCategoryId}")]
        public async Task<ApiResult> RemoveCategory(long subCategoryId)
        {
            var result = await _categoryFacade.RemoveCategory(subCategoryId);
            return CommandResult(result);
        }
    }
}
