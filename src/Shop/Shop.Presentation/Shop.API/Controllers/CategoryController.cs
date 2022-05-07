using Common.Application;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryFacade _categoryFacade;

        public CategoryController(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(long id)
        {
            var result = await _categoryFacade.GetCategoryById(id);
            return Ok(result);
        }

        [HttpGet("getSubCategories/{parentId}")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategoryByParentId(long parentId)
        {
            var result = await _categoryFacade.GetCategoryByParentId(parentId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var result = await _categoryFacade.GetCategoryList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
        {
            var result = await _categoryFacade.Create(command);

            if (result.StatusCode == OperationStatusCode.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory(EditCategoryCommand command)
        {
            var result = await _categoryFacade.Edit(command);

            if (result.StatusCode == OperationStatusCode.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }

        //[HttpDel]
        //public async Task<IActionResult> RemoveCategory(RemoveCategoryCommand command)
        //{
        //    var result = await _categoryFacade.Edit(command);

        //    if (result.StatusCode == OperationStatusCode.Success)
        //        return Ok(result);
        //    else
        //        return BadRequest(result.Message);
        //}

        [HttpPost("AddSubCategory")]
        public async Task<IActionResult> AddSubCategory(AddSubCategoryCommand command)
        {
            var result = await _categoryFacade.AddSubCategory(command);

            if (result.StatusCode == OperationStatusCode.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }

        [HttpDelete("{subCategoryId}")]
        public async Task<IActionResult> RemoveSubCategory(long subCategoryId)
        {
            var result = await _categoryFacade.RemoveSubCategory(subCategoryId);

            if (result.StatusCode == OperationStatusCode.Success)
                return Ok(result);
            else
                return BadRequest(result.Message);
        }
    }
}
