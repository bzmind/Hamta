using System.Text.Json;
using Common.Api;
using Shop.Application.Avatars.Create;
using Shop.Domain.AvatarAggregate;
using Shop.Query.Avatars._DTOs;

namespace Shop.UI.Services.Avatars;

public class AvatarService : IAvatarService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public AvatarService(HttpClient client, JsonSerializerOptions jsonOptions)
    {
        _client = client;
        _jsonOptions = jsonOptions;
    }

    public async Task<ApiResult?> Create(CreateAvatarCommand model)
    {
        var formData = new MultipartFormDataContent();
        formData.Add(new StreamContent(model.AvatarFile.OpenReadStream()), "AvatarFile", model.AvatarFile.FileName);
        formData.Add(new StringContent(model.Gender.ToString()));

        var result = await _client.PostAsync("api/avatar/Create", formData);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult?> Remove(long avatarId)
    {
        var result = await _client.DeleteAsync($"api/avatar/Remove/{avatarId}");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<AvatarDto?> GetById(long avatarId)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<AvatarDto>>($"api/product/GetById/{avatarId}", _jsonOptions);
        return result?.Data;
    }

    public async Task<AvatarDto?> GetByGender(Avatar.AvatarGender gender)
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<AvatarDto>>($"api/product/GetById/{gender}", _jsonOptions);
        return result?.Data;
    }

    public async Task<List<AvatarDto>> GetAll()
    {
        var result = await _client
            .GetFromJsonAsync<ApiResult<List<AvatarDto>>>("api/category/GetAll", _jsonOptions);
        return result.Data;
    }
}