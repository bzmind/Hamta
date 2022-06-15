using Common.Api;
using Shop.Query.Categories._DTOs;
using Shop.UI.Models.Categories;

namespace Shop.UI.Services.Categories;

public interface ICategoryService
{
    Task<ApiResult?> Create(CreateCategoryCommandViewModel model);
    Task<ApiResult?> AddSubCategory(AddSubCategoryCommandViewModel model);
    Task<ApiResult?> Edit(EditCategoryCommandViewModel model);
    Task<ApiResult?> Remove(long categoryId);

    Task<CategoryDto?> GetById(long categoryId);
    Task<List<CategoryDto>?> GetByParentId(long parentId);
    Task<List<CategoryDto>?> GetAll();
}