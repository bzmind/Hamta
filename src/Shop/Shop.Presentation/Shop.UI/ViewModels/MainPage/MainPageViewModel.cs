using Shop.Query.Entities._DTOs;
using Shop.Query.Products._DTOs;

namespace Shop.UI.ViewModels.MainPage;

public class MainPageViewModel
{
    public List<SliderDto> Sliders { get; set; }
    public List<BannerDto> Banners { get; set; }
    public List<ProductForShopDto> SaleProducts { get; set; }
    public List<ProductForShopDto> MostSoldProducts { get; set; }
    public List<ProductForShopDto> CategoryProducts { get; set; }
}