using Common.Api;
using Shop.API.ViewModels.Auth;

namespace Shop.UI.Services.Auth;

public interface IAuthService
{
    Task<ApiResult<LoginResponse>> Login(LoginUserViewModel model);
    Task<ApiResult> Register(RegisterUserViewModel model);
    Task<ApiResult<LoginResponse>> RefreshToken();
    Task<ApiResult> Logout();
}