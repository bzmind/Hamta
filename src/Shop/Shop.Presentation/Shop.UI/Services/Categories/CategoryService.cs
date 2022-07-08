using Common.Api;
using Shop.Query.Categories._DTOs;
using System.Text.Json;
using Shop.Application.Categories.AddSubCategory;
using Shop.Application.Categories.Create;
using Shop.Application.Categories.Edit;

namespace Shop.UI.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public CategoryService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateCategoryCommand model)
    {
        var result = await _client.PostAsJsonAsync("api/category/Create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> AddSubCategory(AddSubCategoryCommand model)
    {
        var result = await _client.PostAsJsonAsync("api/category/AddSubcategory", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditCategoryCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/category/Edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long categoryId)
    {
        var result = await _client.DeleteAsync($"api/category/Remove/{categoryId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<CategoryDto?> GetById(long categoryId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<CategoryDto>>($"api/category/GetById/{categoryId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<CategoryDto>> GetByParentId(long parentId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<CategoryDto>>>
                ($"api/category/GetByParentId/{parentId}", _jsonOptions);
        return result.Data;
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<CategoryDto>>>("api/category/GetAll", _jsonOptions);
        return result.Data;
    }
}