using Microsoft.AspNetCore.Mvc;
using Shop.UI.Services.Auth;
using Shop.UI.SetupClasses.RazorUtility;

namespace Shop.UI.Pages.Auth;

public class LogoutModel : BaseRazorPage
{
    private readonly IAuthService _authService;

    public LogoutModel(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<IActionResult> OnGet()
    {
        var logout = await _authService.Logout();

        if (logout.IsSuccessful)
        {
            HttpContext.Response.Cookies.Delete("token");
            HttpContext.Response.Cookies.Delete("refresh-token");
        }

        return RedirectToPage("../Index");
    }
}