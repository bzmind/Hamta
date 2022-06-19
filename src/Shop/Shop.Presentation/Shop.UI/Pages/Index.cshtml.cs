using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.UI.Pages;

public class IndexModel : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        return Page();
    }
}