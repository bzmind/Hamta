using Common.Api;
using Shop.API.ViewModels.Auth;
using System.Text.Json;

namespace Shop.UI.Services.Auth;

public class AuthService : BaseService, IAuthService
{
    protected override string ApiEndpointName { get; set; } = "Auth";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(HttpClient client, JsonSerializerOptions jsonOptions,
        IHttpContextAccessor httpContextAccessor) : base(client, jsonOptions)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResult<LoginResponse>> Login(LoginUserViewModel model)
    {
        return await PostAsJsonAsync<LoginResponse>("Login", model);
    }

    public async Task<ApiResult> Register(RegisterUserViewModel model)
    {
        return await PostAsJsonAsync<LoginResponse>("Register", model);
    }

    public async Task<ApiResult<LoginResponse>> RefreshToken()
    {
        var refreshToken = _httpContextAccessor.HttpContext!.Request.Cookies["refreshToken"];
        return await PostAsync<LoginResponse>($"RefreshToken={refreshToken}");
    }

    public async Task<ApiResult> Logout()
    {
        return await PostAsync<LoginResponse>("Logout");
    }
}