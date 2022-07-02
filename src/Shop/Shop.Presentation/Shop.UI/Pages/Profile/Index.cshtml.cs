using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.UI.SetupClasses.RazorUtility;

namespace Shop.UI.Pages.Profile;

[Authorize]
[BindProperties]
public class IndexModel : BaseRazorPage
{
    public void OnGet()
    {
    }
}