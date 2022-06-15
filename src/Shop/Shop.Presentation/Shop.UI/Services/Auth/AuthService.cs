using System.Text.Json;
using Common.Api;
using Shop.API.ViewModels.Auth;
using Shop.UI.Models.Auth;
using LoginResponse = Shop.UI.Models.Auth.LoginResponse;

namespace Shop.UI.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(HttpClient client, JsonSerializerOptions jsonOptions,
        IHttpContextAccessor httpContextAccessor)
    {
        _client = client;
        _jsonOptions = jsonOptions;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResult<LoginResponse>?> Login(LoginViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/auth/login", model);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>(_jsonOptions);
    }
    
    public async Task<ApiResult?> Register(RegisterViewModel model)
    {
        var result = await _client.PostAsJsonAsync("api/auth/register", model);
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }

    public async Task<ApiResult<LoginResponse>?> RefreshToken()
    {
        var refreshToken = _httpContextAccessor.HttpContext!.Request.Cookies["refreshToken"];
        var result = await _client.PostAsync($"api/auth/refreshtoken?refreshToken={refreshToken}", null);
        return await result.Content.ReadFromJsonAsync<ApiResult<LoginResponse>>(_jsonOptions);
    }

    public async Task<ApiResult?> Logout()
    {
        var result = await _client.DeleteAsync("api/auth/register");
        return await result.Content.ReadFromJsonAsync<ApiResult>(_jsonOptions);
    }
}