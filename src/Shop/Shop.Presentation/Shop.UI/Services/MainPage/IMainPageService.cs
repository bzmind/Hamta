using Shop.UI.ViewModels.MainPage;

namespace Shop.UI.Services.MainPage;

public interface IMainPageService
{
    Task<MainPageViewModel> GetMainPageData();
}