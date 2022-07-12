using Microsoft.AspNetCore.Mvc;
using Shop.UI.Services.Auth;
using Shop.UI.Setup.RazorUtility;

namespace Shop.UI.Pages.Auth;

public class LogoutModel : BaseRazorPage
{
    private readonly IAuthService _authService;

    public LogoutModel(IAuthService authService,
        IRazorToStringRenderer razorToStringRenderer) : base(razorToStringRenderer)
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