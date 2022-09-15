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

    Task<List<CategoryDto>> GetAll();
    Task<List<CategoryDto>> GetForMenu();
    Task<CategoryDto?> GetById(long categoryId);
    Task<List<CategoryDto>> GetByParentId(long parentId);
    Task<List<CategorySpecificationQueryDto>> GetSpecificationsByCategoryId(long categoryId);
}