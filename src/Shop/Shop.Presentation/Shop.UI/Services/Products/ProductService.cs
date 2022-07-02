﻿using Common.Api;
using Shop.Query.Products._DTOs;
using System.Text;
using System.Text.Json;
using Common.Application.Utility.Validation.CustomAttributes;
using Shop.Application.Products.AddScore;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveGalleryImage;
using Shop.Application.Products.ReplaceMainImage;

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

    public async Task<ApiResult?> Create(CreateProductCommand model)
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

        var extraDescriptionsJson = JsonSerializer.Serialize(model.ExtraDescriptions);
        formData.Add(new StringContent(extraDescriptionsJson, Encoding.UTF8, "application/json"), "ExtraDescriptions");

        var result = await _client.PostAsync("api/product/Create", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Edit(EditProductCommand model)
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

        var extraDescriptionsJson = JsonSerializer.Serialize(model.ExtraDescriptions);
        formData.Add(new StringContent(extraDescriptionsJson, Encoding.UTF8, "application/json"), "ExtraDescriptions");

        var result = await _client.PutAsync("api/product/Edit", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> ReplaceMainImage(ReplaceProductMainImageCommand model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.ProductId.ToString()), "ProductId");

        if (model.MainImage.IsImage())
            formData.Add(new StreamContent(model.MainImage.OpenReadStream()), "MainImage", model.MainImage.FileName);

        var result = await _client.PutAsync("api/product/ReplaceMainImage", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> AddScore(AddProductScoreCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/product/AddScore", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> RemoveGalleryImage(RemoveProductGalleryImageCommand model)
    {
        var result = await _client.PutAsJsonAsync("api/product/RemoveGalleryImage", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long productId)
    {
        var result = await _client.DeleteAsync($"api/product/Remove/{productId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ProductDto?> GetById(long productId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<ProductDto>>($"api/product/GetById/{productId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<ProductFilterResult?> GetByFilter(ProductFilterParams filterParams)
    {
        var url = $"api/product/GetByFilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&CategoryId={filterParams.CategoryId}&Name={filterParams.Name}" +
                  $"&EnglishName={filterParams.EnglishName}&Slug={filterParams.Slug}" +
                  $"&AverageScore={filterParams.AverageScore}";

        var result = await _client.GetFromJsonAsync<ApiResult<ProductFilterResult>>(url, _jsonOptions);
        return result?.Data;
    }
}