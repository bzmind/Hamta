using Common.Api;
using Shop.Query.Products._DTOs;
using System.Text;
using System.Text.Json;
using Common.Application.Utility.Validation.CustomAttributes;
using Shop.API.ViewModels.Products;

namespace Shop.UI.Services.Products;

public class ProductService : BaseService, IProductService
{
    protected override string ApiEndpointName { get; set; } = "Product";

    public ProductService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateProductViewModel model)
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

        var specificationsJson = JsonSerializer.Serialize(model.Specifications);
        formData.Add(new StringContent(specificationsJson, Encoding.UTF8, "application/json"), "Specifications");

        var extraDescriptionsJson = JsonSerializer.Serialize(model.ExtraDescriptions);
        formData.Add(new StringContent(extraDescriptionsJson, Encoding.UTF8, "application/json"), "ExtraDescriptions");

        return await PostAsFormDataAsync("Create", formData);
    }

    public async Task<ApiResult> Edit(EditProductViewModel model)
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

        var specificationsJson = JsonSerializer.Serialize(model.Specifications);
        formData.Add(new StringContent(specificationsJson, Encoding.UTF8, "application/json"), "Specifications");

        var extraDescriptionsJson = JsonSerializer.Serialize(model.ExtraDescriptions);
        formData.Add(new StringContent(extraDescriptionsJson, Encoding.UTF8, "application/json"), "ExtraDescriptions");

        return await PutAsFormDataAsync("Edit", formData);
    }

    public async Task<ApiResult> ReplaceMainImage(ReplaceProductMainImageViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.ProductId.ToString()), "ProductId");

        if (model.MainImage.IsImage())
            formData.Add(new StreamContent(model.MainImage.OpenReadStream()), "MainImage", model.MainImage.FileName);

        return await PutAsFormDataAsync("ReplaceMainImage", formData);
    }

    public async Task<ApiResult> AddScore(AddProductScoreViewModel model)
    {
        return await PutAsJsonAsync("AddScore", model);
    }

    public async Task<ApiResult> RemoveGalleryImage(RemoveProductGalleryImageViewModel model)
    {
        return await PutAsJsonAsync("RemoveGalleryImage", model);
    }

    public async Task<ApiResult> Remove(long productId)
    {
        return await DeleteAsync($"Remove/{productId}");
    }

    public async Task<ProductDto?> GetById(long productId)
    {
        var result = await GetFromJsonAsync<ProductDto>($"GetById/{productId}");
        return result.Data;
    }

    public async Task<ProductFilterResult> GetByFilter(ProductFilterParams filterParams)
    {
        var url = $"GetByFilter?PageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&CategoryId={filterParams.CategoryId}&Name={filterParams.Name}" +
                  $"&EnglishName={filterParams.EnglishName}&Slug={filterParams.Slug}" +
                  $"&AverageScore={filterParams.AverageScore}&MinPrice={filterParams.MinPrice}" +
                  $"&MaxPrice={filterParams.MaxPrice}";
        var result = await GetFromJsonAsync<ProductFilterResult>(url);
        return result.Data;
    }
}