using Common.Application;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;
using Shop.Application.Categories.RemoveSubCategory;
using Shop.Query.Categories._DTOs;

namespace Shop.Presentation.Facade.Categories;

public interface ICategoryFacade
{
    Task<OperationResult> Create(CreateCategoryCommand command);
    Task<OperationResult> Edit(EditCategoryCommand command);
    Task<OperationResult> AddSubCategory(AddSubCategoryCommand command);
    Task<OperationResult> RemoveSubCategory(RemoveSubCategoryCommand command);

    Task<CategoryDto?> GetCategoryById(long id);
    Task<List<CategoryDto>> GetCategoryByParentId(long parentId);
    Task<List<CategoryDto>> GetCategoryList();
}