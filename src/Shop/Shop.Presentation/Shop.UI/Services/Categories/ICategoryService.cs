using Common.Api;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Query.Categories._DTOs;

namespace Shop.UI.Services.Categories;

public interface ICategoryService
{
    Task<ApiResult?> Create(CreateCategoryCommand model);
    Task<ApiResult?> AddSubCategory(AddSubCategoryCommand model);
    Task<ApiResult?> Edit(EditCategoryCommand model);
    Task<ApiResult?> Remove(long categoryId);

    Task<CategoryDto?> GetById(long categoryId);
    Task<List<CategoryDto>?> GetByParentId(long parentId);
    Task<List<CategoryDto>?> GetAll();
}