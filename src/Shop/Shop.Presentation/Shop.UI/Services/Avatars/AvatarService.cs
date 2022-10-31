using System.Text.Json;
using Common.Api;
using Shop.API.ViewModels.Avatars;
using Shop.Domain.AvatarAggregate;
using Shop.Query.Avatars._DTOs;

namespace Shop.UI.Services.Avatars;

public class AvatarService : BaseService, IAvatarService
{
    protected override string ApiEndpointName { get; set; } = "Avatar";

    public AvatarService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateAvatarViewModel model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(model.AvatarFile.OpenReadStream()), "AvatarFile", model.AvatarFile.FileName);
        formData.Add(new StringContent(model.Gender.ToString()), "Gender");
        return await PostAsFormDataAsync("Create", formData);
    }

    public async Task<ApiResult> Remove(long avatarId)
    {
        return await DeleteAsync($"Remove/{avatarId}");
    }

    public async Task<ApiResult<AvatarDto?>> GetById(long avatarId)
    {
        var result = await GetFromJsonAsync<AvatarDto>($"GetById/{avatarId}");
        return result;
    }

    public async Task<ApiResult<AvatarDto?>> GetByGender(Avatar.AvatarGender gender)
    {
        var result = await GetFromJsonAsync<AvatarDto>($"GetByGender/{gender}");
        return result;
    }

    public async Task<ApiResult<List<AvatarDto>>> GetAll()
    {
        var result = await GetFromJsonAsync<List<AvatarDto>>("GetAll");
        return result;
    }
}