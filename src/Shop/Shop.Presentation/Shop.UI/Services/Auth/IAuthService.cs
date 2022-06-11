using Common.Api;
using Shop.UI.Models.Auth;

namespace Shop.UI.Services.Auth;

public interface IAuthService
{
    Task<ApiResult<LoginResponse>?> Login(LoginViewModel model);
    Task<ApiResult?> Register(RegisterViewModel model);
    Task<ApiResult<LoginResponse>?> RefreshToken();
    Task<ApiResult?> Logout();
}