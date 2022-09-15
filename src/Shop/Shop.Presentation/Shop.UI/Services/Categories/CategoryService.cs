using Common.Api;
using Shop.Query.Categories._DTOs;
using System.Text.Json;
using Shop.API.ViewModels.Categories;

namespace Shop.UI.Services.Categories;

public class CategoryService : BaseService, ICategoryService
{
    protected override string ApiEndpointName { get; set; } = "Category";

    public CategoryService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateCategoryViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> AddSubCategory(AddSubCategoryViewModel model)
    {
        return await PostAsJsonAsync("AddSubcategory", model);
    }

    public async Task<ApiResult> Edit(EditCategoryViewModel model)
    {
        return await PutAsJsonAsync("Edit", model);
    }

    public async Task<ApiResult> Remove(long categoryId)
    {
        return await DeleteAsync($"Remove/{categoryId}");
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        var result = await GetFromJsonAsync<List<CategoryDto>>("GetAll");
        return result.Data;
    }

    public async Task<List<CategoryDto>> GetForMenu()
    {
        var result = await GetFromJsonAsync<List<CategoryDto>>("GetForMenu");
        return result.Data;
    }

    public async Task<CategoryDto?> GetById(long categoryId)
    {
        var result = await GetFromJsonAsync<CategoryDto>($"GetById/{categoryId}");
        return result.Data;
    }

    public async Task<List<CategoryDto>> GetByParentId(long parentId)
    {
        var result = await GetFromJsonAsync<List<CategoryDto>>($"GetByParentId/{parentId}");
        return result.Data;
    }

    public async Task<List<CategorySpecificationQueryDto>> GetSpecificationsByCategoryId(long categoryId)
    {
        var result = await GetFromJsonAsync<List<CategorySpecificationQueryDto>>
            ($"GetSpecificationsByCategoryId/{categoryId}");
        return result.Data;
    }
}