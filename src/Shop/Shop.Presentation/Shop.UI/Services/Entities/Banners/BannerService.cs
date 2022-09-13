using System.Text.Json;
using Common.Api;
using Common.Application.Utility.Validation.CustomAttributes;
using Shop.API.ViewModels.Entities.Banner;
using Shop.Query.Entities._DTOs;

namespace Shop.UI.Services.Entities.Banners;

public class BannerService : BaseService, IBannerService
{
    protected override string ApiEndpointName { get; set; } = "Banner";

    public BannerService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateBannerViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.Link), "Link");
        formData.Add(new StreamContent(model.Image.OpenReadStream()), "Image", model.Image.FileName);
        formData.Add(new StringContent(model.Position.ToString()), "Position");
        return await PostAsFormDataAsync("Create", formData);
    }

    public async Task<ApiResult> Edit(EditBannerViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.Id.ToString()), "Id");
        formData.Add(new StringContent(model.Link), "Link");
        if (model.Image != null && model.Image.IsImage())
            formData.Add(new StreamContent(model.Image.OpenReadStream()), "Image", model.Image.FileName);
        formData.Add(new StringContent(model.Position.ToString()), "Position");
        return await PutAsFormDataAsync("Edit", formData);
    }

    public async Task<ApiResult> Remove(long id)
    {
        return await DeleteAsync($"Remove/{id}");
    }

    public async Task<BannerDto?> GetById(long id)
    {
        var result = await GetFromJsonAsync<BannerDto>($"GetById/{id}");
        return result.Data;
    }

    public async Task<List<BannerDto>> GetAll()
    {
        var result = await GetFromJsonAsync<List<BannerDto>>("GetAll");
        return result.Data;
    }
}