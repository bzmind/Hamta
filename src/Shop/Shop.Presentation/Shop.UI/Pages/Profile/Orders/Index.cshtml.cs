using Microsoft.AspNetCore.Authorization;
using Shop.UI.SetupClasses.RazorUtility;

namespace Shop.UI.Pages.Profile.Orders;

[Authorize]
public class IndexModel : BaseRazorPage
{
    public void OnGet()
    {
    }
}