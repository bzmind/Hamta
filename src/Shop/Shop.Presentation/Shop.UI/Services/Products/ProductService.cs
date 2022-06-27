using Common.Api;
using Common.Application.Utility.Validation.CustomAttributes;
using Shop.Query.Products._DTOs;
using Shop.UI.Models.Products;
using System.Text;
using System.Text.Json;

namespace Shop.UI.Services.Products;

public class ProductService : IProductService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public ProductService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateProductCommandViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.CategoryId.ToString()), "CategoryId");
        formData.Add(new StringContent(model.Name), "Name");
        formData.Add(new StringContent(model.Slug), "Slug");

        if (model.MainImage.IsImage())
            formData.Add(new StreamContent(model.MainImage.OpenReadStream()), "MainImage", model.MainImage.FileName);

        if (model.EnglishName != null)
            formData.Add(new StringContent(model.EnglishName), "EnglishName");

        if (model.Description != null)
            formData.Add(new StringContent(model.Description), "Description");

        model.GalleryImages.ForEach(galleryImage =>
        {
            if (galleryImage.IsImage())
                formData.Add(new StreamContent(galleryImage.OpenReadStream()), "GalleryImage", galleryImage.FileName);
        });

        var specificationsJson = JsonSerializer.Serialize(model.CustomSpecifications);
        formData.Add(new StringContent(specificationsJson, Encoding.UTF8, "application/json"), "CustomSpecifications");

        var extraDescriptionsJson = JsonSerializer.Serialize(model.ExtraDescription);
        formData.Add(new StringContent(extraDescriptionsJson, Encoding.UTF8, "application/json"), "ExtraDescriptions");

        var result = await _client.PostAsync("api/product/create", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditProductCommandViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.ProductId.ToString()), "ProductId");
        formData.Add(new StringContent(model.CategoryId.ToString()), "CategoryId");
        formData.Add(new StringContent(model.Name), "Name");
        formData.Add(new StringContent(model.Slug), "Slug");

        if (model.MainImage.IsImage())
            formData.Add(new StreamContent(model.MainImage.OpenReadStream()), "MainImage", model.MainImage.FileName);

        if (model.EnglishName != null)
            formData.Add(new StringContent(model.EnglishName), "EnglishName");

        if (model.Description != null)
            formData.Add(new StringContent(model.Description), "Description");

        model.GalleryImages.ForEach(galleryImage =>
        {
            if (galleryImage.IsImage())
                formData.Add(new StreamContent(galleryImage.OpenReadStream()), "GalleryImage", galleryImage.FileName);
        });

        var specificationsJson = JsonSerializer.Serialize(model.CustomSpecifications);
        formData.Add(new StringContent(specificationsJson, Encoding.UTF8, "application/json"), "CustomSpecifications");

        var extraDescriptionsJson = JsonSerializer.Serialize(model.ExtraDescription);
        formData.Add(new StringContent(extraDescriptionsJson, Encoding.UTF8, "application/json"), "ExtraDescriptions");

        var result = await _client.PutAsync("api/product/edit", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> ReplaceMainImage(ReplaceProductMainImageCommandViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.ProductId.ToString()), "ProductId");

        if (model.MainImage.IsImage())
            formData.Add(new StreamContent(model.MainImage.OpenReadStream()), "MainImage", model.MainImage.FileName);

        var result = await _client.PutAsync("api/product/replacemainimage", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> AddScore(AddProductScoreCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/product/addscore", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> RemoveGalleryImage(RemoveProductGalleryImageCommandViewModel model)
    {
        var result = await _client.PutAsJsonAsync("api/product/removegalleryimage", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long productId)
    {
        var result = await _client.DeleteAsync($"api/product/remove/{productId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ProductDto?> GetById(long productId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<ProductDto>>($"api/product/getbyid/{productId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<ProductFilterResult?> GetByFilter(ProductFilterParamsViewModel filterParams)
    {
        var url = $"api/product/getbyfilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&CategoryId={filterParams.CategoryId}&Name={filterParams.Name}" +
                  $"&EnglishName={filterParams.EnglishName}&Slug={filterParams.Slug}" +
                  $"&AverageScore={filterParams.AverageScore}";

        var result = await _client.GetFromJsonAsync<ApiResult<ProductFilterResult>>(url, _jsonOptions);
        return result?.Data;
    }
}