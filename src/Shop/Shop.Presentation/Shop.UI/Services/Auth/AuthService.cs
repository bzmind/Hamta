using Common.Api;
using Shop.API.ViewModels.Auth;
using System.Text.Json;

namespace Shop.UI.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(HttpClient client, JsonSerializerOptions jsonOptions, IHttpContextAccessor httpContextAccessor)
    {
        _client = client;
        _jsonOptions = jsonOptions;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResult<LoginResponse>?> Login(LoginViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/auth/Login", model);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>(_jsonOptions);
    }

    public async Task<ApiResult?> Register(RegisterViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/auth/Register", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult<LoginResponse>?> RefreshToken()
    {
        var refreshToken = _httpContextAccessor.HttpContext!.Request.Cookies["refreshToken"];
        var result = await _client.PostAsync($"api/auth/RefreshToken?refreshToken={refreshToken}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>(_jsonOptions);
    }

    public async Task<ApiResult?> Logout()
    {
        var result = await _client.PostAsync("api/auth/Logout", null);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }
}