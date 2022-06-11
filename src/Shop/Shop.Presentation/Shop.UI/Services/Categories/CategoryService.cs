using System.Text.Json;
using Common.Api;
using Shop.Query.Categories._DTOs;
using Shop.UI.Models.Categories;

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

    public async Task<ApiResult?> Create(CreateCategoryViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/category/create", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> AddSubCategory(AddSubCategoryViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/category/addsubcategory", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditCategoryViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/category/edit", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long categoryId)
    {
        var result = await _client.DeleteAsync($"api/category/remove/{categoryId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<CategoryDto?> GetById(long categoryId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<CategoryDto>>($"api/category/getbyid/{categoryId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<CategoryDto>?> GetByParentId(long parentId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<CategoryDto>>>
                ($"api/category/getbyparentid/{parentId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<CategoryDto>?> GetAll()
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<CategoryDto>>>("api/category/getall", _jsonOptions);
        return result?.Data;
    }
}