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

        if (model.Introduction != null)
            formData.Add(new StringContent(model.Introduction), "Introduction");

        if (model.Review != null)
            formData.Add(new StringContent(model.Review), "Review");

        model.GalleryImages.ForEach(image =>
        {
            if (image.IsImage())
                formData.Add(new StreamContent(image.OpenReadStream()), "GalleryImages", image.FileName);
        });

        var specificationsJson = JsonSerializer.Serialize(model.Specifications);
        formData.Add(new StringContent(specificationsJson, Encoding.UTF8, "application/json"), "SpecificationsJson");

        var categorySpecificationsJson = JsonSerializer.Serialize(model.CategorySpecifications);
        formData.Add(new StringContent(categorySpecificationsJson, Encoding.UTF8, "application/json"), "CategorySpecificationsJson");

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

        if (model.Introduction != null)
            formData.Add(new StringContent(model.Introduction), "Introduction");

        if (model.Review != null)
            formData.Add(new StringContent(model.Review), "Review");

        model.GalleryImages.ForEach(image =>
        {
            if (image.IsImage())
                formData.Add(new StreamContent(image.OpenReadStream()), "GalleryImages", image.FileName);
        });

        var specifications = JsonSerializer.Serialize(model.Specifications);
        formData.Add(new StringContent(specifications, Encoding.UTF8, "application/json"), "SpecificationsJson");

        var categorySpecificationsJson = JsonSerializer.Serialize(model.CategorySpecifications);
        formData.Add(new StringContent(categorySpecificationsJson, Encoding.UTF8, "application/json"), "CategorySpecificationsJson");

        return await PutAsFormDataAsync("Edit", formData);
    }

    public async Task<ApiResult> AddScore(AddProductScoreViewModel model)
    {
        return await PutAsJsonAsync("AddScore", model);
    }

    public async Task<ApiResult> Remove(long productId)
    {
        return await DeleteAsync($"Remove/{productId}");
    }

    public async Task<ApiResult<string?>> AddReviewImage(AddProductReviewImageViewModel model)
    {
        var formData = new MultipartFormDataContent();
        if (model.Image.IsImage())
            formData.Add(new StreamContent(model.Image.OpenReadStream()), "Image", model.Image.FileName);
        return await PostAsFormDataAsync<string?>("AddReviewImage", formData);
    }

    public async Task<ProductFilterResult> GetByFilter(ProductFilterParams filterParams)
    {
        var url = MakeQueryUrl("GetByFilter", filterParams);
        var result = await GetFromJsonAsync<ProductFilterResult>(url);
        return result.Data;
    }

    public async Task<ProductForShopResult> GetForShopByFilter(ProductForShopFilterParams filterFilterParams)
    {
        var url = MakeQueryUrl("GetForShopByFilter", filterFilterParams);
        for (var i = 0; i < filterFilterParams.Attributes?.Count; i++)
        {
            var attr = filterFilterParams.Attributes?[i];
            url += $"&Attributes={attr}";
        }
        var result = await GetFromJsonAsync<ProductForShopResult>(url);
        return result.Data;
    }

    public async Task<ProductDto?> GetById(long productId)
    {
        var result = await GetFromJsonAsync<ProductDto>($"GetById/{productId}");
        return result.Data;
    }

    public async Task<SingleProductDto?> GetSingleBySlug(string slug)
    {
        var result = await GetFromJsonAsync<SingleProductDto>($"GetSingleBySlug/{slug}");
        return result.Data;
    }
}