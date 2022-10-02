using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.UI.Services.MainPage;
using Shop.UI.ViewModels.MainPage;

namespace Shop.UI.Pages;

public class IndexModel : PageModel
{
    private readonly IMainPageService _mainPageService;

    public IndexModel(IMainPageService mainPageService)
    {
        _mainPageService = mainPageService;
    }

    public MainPageViewModel PageData { get; set; }

    public async Task OnGet()
    {
        PageData = await _mainPageService.GetMainPageData();
    }
}