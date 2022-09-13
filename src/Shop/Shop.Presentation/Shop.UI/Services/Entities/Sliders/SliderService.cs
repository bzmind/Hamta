using System.Text.Json;
using Common.Api;
using Common.Application.Utility.Validation.CustomAttributes;
using Shop.API.ViewModels.Entities.Slider;
using Shop.Query.Entities._DTOs;

namespace Shop.UI.Services.Entities.Sliders;

public class SliderService : BaseService, ISliderService
{
    protected override string ApiEndpointName { get; set; } = "Slider";

    public SliderService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateSliderViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.Title), "Title");
        formData.Add(new StringContent(model.Link), "Link");
        formData.Add(new StreamContent(model.Image.OpenReadStream()), "Image", model.Image.FileName);
        return await PostAsFormDataAsync("Create", formData);
    }

    public async Task<ApiResult> Edit(EditSliderViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(model.Id.ToString()), "Id");
        formData.Add(new StringContent(model.Title), "Title");
        formData.Add(new StringContent(model.Link), "Link");
        if (model.Image != null && model.Image.IsImage())
            formData.Add(new StreamContent(model.Image.OpenReadStream()), "Image", model.Image.FileName);
        return await PutAsFormDataAsync("Edit", formData);
    }

    public async Task<ApiResult> Remove(long id)
    {
        return await DeleteAsync($"Remove/{id}");
    }

    public async Task<SliderDto?> GetById(long id)
    {
        var result = await GetFromJsonAsync<SliderDto>($"GetById/{id}");
        return result.Data;
    }

    public async Task<List<SliderDto>> GetAll()
    {
        var result = await GetFromJsonAsync<List<SliderDto>>("GetAll");
        return result.Data;
    }
}