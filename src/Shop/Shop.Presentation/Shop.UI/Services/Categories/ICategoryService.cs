using Common.Api;
using Shop.API.ViewModels.Categories;
using Shop.Query.Categories._DTOs;

namespace Shop.UI.Services.Categories;

public interface ICategoryService
{
    Task<ApiResult> Create(CreateCategoryViewModel model);
    Task<ApiResult> AddSubCategory(AddSubCategoryViewModel model);
    Task<ApiResult> Edit(EditCategoryViewModel model);
    Task<ApiResult> Remove(long categoryId);

    Task<ApiResult<List<CategoryDto>>> GetAll();
    Task<ApiResult<List<CategoryDto>>> GetForMenu();
    Task<ApiResult<CategoryDto?>> GetById(long categoryId);
    Task<ApiResult<List<CategoryDto>>> GetByParentId(long parentId);
    Task<ApiResult<List<CategorySpecificationQueryDto>>> GetSpecificationsByCategoryId(long categoryId);
}